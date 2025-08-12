using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using HrSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HrSystem.Infrastructure.Persistence.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(
        HrSystemDbContext db,
        ILogger logger,
        CancellationToken cancellationToken = default
    )
    {
        // Apply pending migrations first
        await db.Database.MigrateAsync(cancellationToken);

        // Seed base lookup data
        if (!await db.OrgTypes.AnyAsync(cancellationToken))
        {
            logger.LogInformation("Seeding OrgTypes...");
            var orgTypes = new[]
            {
                new OrgType("Division"),
                new OrgType("Department"),
                new OrgType("Team"),
            };
            await db.OrgTypes.AddRangeAsync(orgTypes, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }

        // Seed OrgUnits
        if (!await db.OrgUnits.AnyAsync(cancellationToken))
        {
            logger.LogInformation("Seeding OrgUnits...");
            var divisionTypeId = await db
                .OrgTypes.Where(x => x.Name == "Division")
                .Select(x => x.Id)
                .FirstAsync(cancellationToken);
            var departmentTypeId = await db
                .OrgTypes.Where(x => x.Name == "Department")
                .Select(x => x.Id)
                .FirstAsync(cancellationToken);
            var teamTypeId = await db
                .OrgTypes.Where(x => x.Name == "Team")
                .Select(x => x.Id)
                .FirstAsync(cancellationToken);

            // 1) Root
            var corpDiv = new OrgUnit("Corporate", divisionTypeId);
            await db.OrgUnits.AddAsync(corpDiv, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            // 2) Departments under Corporate
            var engDept = new OrgUnit("Engineering", departmentTypeId, parentId: corpDiv.Id);
            var salesDept = new OrgUnit("Sales", departmentTypeId, parentId: corpDiv.Id);
            await db.OrgUnits.AddRangeAsync(new[] { engDept, salesDept }, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            // 3) Teams under departments
            var platformTeam = new OrgUnit("Platform", teamTypeId, parentId: engDept.Id);
            var appsTeam = new OrgUnit("Applications", teamTypeId, parentId: engDept.Id);
            var emeaSalesTeam = new OrgUnit("EMEA Sales", teamTypeId, parentId: salesDept.Id);
            await db.OrgUnits.AddRangeAsync(
                new[] { platformTeam, appsTeam, emeaSalesTeam },
                cancellationToken
            );
            await db.SaveChangesAsync(cancellationToken);
        }

        // Seed Employees
        if (!await db.Employees.AnyAsync(cancellationToken))
        {
            logger.LogInformation("Seeding Employees...");
            var allUnits = await db.OrgUnits.AsNoTracking().ToListAsync(cancellationToken);
            var rnd = new Random(42);

            string Email(string first, string last, int index) =>
                $"{first.ToLower()}.{last.ToLower()}{index}@example.com";

            var firstNames = new[]
            {
                "Alice",
                "Bob",
                "Carol",
                "Dave",
                "Eve",
                "Frank",
                "Grace",
                "Heidi",
                "Ivan",
                "Judy",
            };
            var lastNames = new[]
            {
                "Anderson",
                "Brown",
                "Clark",
                "Davis",
                "Evans",
                "Foster",
                "Garcia",
                "Harris",
                "Ivanov",
                "Johnson",
            };

            var employees = new List<Employee>();
            for (int i = 0; i < 12; i++)
            {
                var fn = firstNames[i % firstNames.Length];
                var ln = lastNames[i % lastNames.Length];
                var unit = allUnits[rnd.Next(allUnits.Count)];
                var hire = DateOnly.FromDateTime(
                    DateTime.UtcNow.Date.AddDays(-rnd.Next(30, 365 * 5))
                );
                var emp = new Employee(
                    fn,
                    ln,
                    Email(fn, ln, i),
                    hire,
                    unit.Id,
                    EmployeeStatus.Active
                );
                employees.Add(emp);
            }

            await db.Employees.AddRangeAsync(employees, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            // Assign managers to some org units (first employee found in that unit)
            logger.LogInformation("Assigning OrgUnit managers...");
            var unitsToManage = await db.OrgUnits.ToListAsync(cancellationToken);
            foreach (var unit in unitsToManage)
            {
                var manager = await db
                    .Employees.Where(e => e.OrgUnitId == unit.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (manager != null)
                {
                    unit.ManagerId = manager.Id;
                }
            }
            await db.SaveChangesAsync(cancellationToken);
        }

        // Seed Leave Requests
        if (!await db.LeaveRequests.AnyAsync(cancellationToken))
        {
            logger.LogInformation("Seeding LeaveRequests...");
            var someEmployees = await db.Employees.Take(6).ToListAsync(cancellationToken);
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            var leaves = new List<LeaveRequest>();
            for (int i = 0; i < someEmployees.Count; i++)
            {
                var start = today.AddDays(7 + i * 3);
                var end = start.AddDays(2 + (i % 3));
                leaves.Add(
                    new LeaveRequest(someEmployees[i].Id, new DateRange(start, end), "Vacation")
                );
            }
            await db.LeaveRequests.AddRangeAsync(leaves, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }

        logger.LogInformation("Database seeding completed.");
    }
}
