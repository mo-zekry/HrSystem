using HrSystem.Domain.Entities;
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

        // Seed OrgTypes
        if (!db.OrgTypes.Any())
        {
            db.OrgTypes.AddRange(
                new OrgType("Company"),
                new OrgType("Department"),
                new OrgType("Team")
            );
            await db.SaveChangesAsync(cancellationToken);
        }

        // Seed OrgUnits
        if (!db.OrgUnits.Any())
        {
            var orgTypes = db.OrgTypes.ToList();
            var hqType = orgTypes.First(x => x.Name == "Company");
            var deptType = orgTypes.First(x => x.Name == "Department");
            var teamType = orgTypes.First(x => x.Name == "Team");

            // Main company
            var hq = new OrgUnit("Main Company", hqType.Id);
            db.OrgUnits.Add(hq);
            await db.SaveChangesAsync(cancellationToken);

            var dept1 = new OrgUnit("IT Department", deptType.Id, hq.Id);
            var dept2 = new OrgUnit("HR Department", deptType.Id, hq.Id);
            db.OrgUnits.AddRange(dept1, dept2);
            await db.SaveChangesAsync(cancellationToken);

            // Big company: Hayaricy
            var hayaricy = new OrgUnit("Hayaricy", hqType.Id);
            db.OrgUnits.Add(hayaricy);
            await db.SaveChangesAsync(cancellationToken);

            var hayaricyIT = new OrgUnit("Hayaricy IT", deptType.Id, hayaricy.Id);
            var hayaricyHR = new OrgUnit("Hayaricy HR", deptType.Id, hayaricy.Id);
            var hayaricySales = new OrgUnit("Hayaricy Sales", deptType.Id, hayaricy.Id);
            db.OrgUnits.AddRange(hayaricyIT, hayaricyHR, hayaricySales);
            await db.SaveChangesAsync(cancellationToken);

            var hayaricyDevTeamA = new OrgUnit("Hayaricy Dev Team A", teamType.Id, hayaricyIT.Id);
            var hayaricyDevTeamB = new OrgUnit("Hayaricy Dev Team B", teamType.Id, hayaricyIT.Id);
            var hayaricySupportTeam = new OrgUnit(
                "Hayaricy Support Team",
                teamType.Id,
                hayaricyHR.Id
            );
            db.OrgUnits.AddRange(hayaricyDevTeamA, hayaricyDevTeamB, hayaricySupportTeam);
            await db.SaveChangesAsync(cancellationToken);
        }

        // Seed Employees
        if (!db.Employees.Any())
        {
            var orgUnits = db.OrgUnits.ToList();
            var hq = orgUnits.First(x => x.Name == "Main Company");
            var itDept = orgUnits.First(x => x.Name == "IT Department");
            var hrDept = orgUnits.First(x => x.Name == "HR Department");

            var hayaricy = orgUnits.First(x => x.Name == "Hayaricy");
            var hayaricyIT = orgUnits.First(x => x.Name == "Hayaricy IT");
            var hayaricyHR = orgUnits.First(x => x.Name == "Hayaricy HR");
            var hayaricySales = orgUnits.First(x => x.Name == "Hayaricy Sales");
            var hayaricyDevTeamA = orgUnits.First(x => x.Name == "Hayaricy Dev Team A");
            var hayaricyDevTeamB = orgUnits.First(x => x.Name == "Hayaricy Dev Team B");
            var hayaricySupportTeam = orgUnits.First(x => x.Name == "Hayaricy Support Team");

            db.Employees.AddRange(
                // Main company employees
                new Employee(
                    "John",
                    "Doe",
                    "john.doe@example.com",
                    "مدير",
                    "Manager",
                    new DateOnly(2020, 1, 10),
                    hq.Id
                ),
                new Employee(
                    "Jane",
                    "Smith",
                    "jane.smith@example.com",
                    "مبرمج",
                    "Developer",
                    new DateOnly(2021, 5, 20),
                    itDept.Id
                ),
                new Employee(
                    "Ali",
                    "Hassan",
                    "ali.hassan@example.com",
                    "موارد بشرية",
                    "HR",
                    new DateOnly(2022, 3, 15),
                    hrDept.Id
                ),
                // Hayaricy company employees
                new Employee(
                    "Mohamed",
                    "Zekry",
                    "mohamed.zekry@hayaricy.com",
                    "المدير التنفيذي",
                    "CEO",
                    new DateOnly(2018, 2, 1),
                    hayaricy.Id
                ),
                new Employee(
                    "Sara",
                    "Ali",
                    "sara.ali@hayaricy.com",
                    "مدير تقنية المعلومات",
                    "IT Manager",
                    new DateOnly(2019, 6, 15),
                    hayaricyIT.Id
                ),
                new Employee(
                    "Omar",
                    "Khaled",
                    "omar.khaled@hayaricy.com",
                    "مدير الموارد البشرية",
                    "HR Manager",
                    new DateOnly(2020, 3, 10),
                    hayaricyHR.Id
                ),
                new Employee(
                    "Lina",
                    "Youssef",
                    "lina.youssef@hayaricy.com",
                    "مدير المبيعات",
                    "Sales Manager",
                    new DateOnly(2021, 1, 5),
                    hayaricySales.Id
                ),
                new Employee(
                    "Ahmed",
                    "Samir",
                    "ahmed.samir@hayaricy.com",
                    "مطور أول",
                    "Senior Developer",
                    new DateOnly(2020, 7, 20),
                    hayaricyDevTeamA.Id
                ),
                new Employee(
                    "Fatma",
                    "Hassan",
                    "fatma.hassan@hayaricy.com",
                    "مطور",
                    "Developer",
                    new DateOnly(2021, 8, 12),
                    hayaricyDevTeamA.Id
                ),
                new Employee(
                    "Karim",
                    "Nabil",
                    "karim.nabil@hayaricy.com",
                    "مطور",
                    "Developer",
                    new DateOnly(2022, 2, 18),
                    hayaricyDevTeamB.Id
                ),
                new Employee(
                    "Mona",
                    "Saeed",
                    "mona.saeed@hayaricy.com",
                    "دعم فني",
                    "Support",
                    new DateOnly(2022, 5, 10),
                    hayaricySupportTeam.Id
                ),
                new Employee(
                    "Hossam",
                    "Fathy",
                    "hossam.fathy@hayaricy.com",
                    "دعم فني",
                    "Support",
                    new DateOnly(2023, 3, 22),
                    hayaricySupportTeam.Id
                )
            );
            await db.SaveChangesAsync(cancellationToken);
        }

        // Seed LeaveRequests
        if (!db.LeaveRequests.Any())
        {
            var employees = db.Employees.ToList();
            db.LeaveRequests.AddRange(
                // Main company
                new LeaveRequest(
                    employees[0].Id,
                    new DateRange(new DateOnly(2025, 8, 1), new DateOnly(2025, 8, 10)),
                    "Annual leave"
                ),
                new LeaveRequest(
                    employees[1].Id,
                    new DateRange(new DateOnly(2025, 9, 5), new DateOnly(2025, 9, 7)),
                    "Sick leave"
                ),
                // Hayaricy company
                new LeaveRequest(
                    employees[3].Id, // Mohamed Zekry
                    new DateRange(new DateOnly(2025, 7, 15), new DateOnly(2025, 7, 20)),
                    "Business trip"
                ),
                new LeaveRequest(
                    employees[4].Id, // Sara Ali
                    new DateRange(new DateOnly(2025, 8, 2), new DateOnly(2025, 8, 5)),
                    "Annual leave"
                ),
                new LeaveRequest(
                    employees[7].Id, // Ahmed Samir
                    new DateRange(new DateOnly(2025, 9, 1), new DateOnly(2025, 9, 10)),
                    "Annual leave"
                ),
                new LeaveRequest(
                    employees[10].Id, // Mona Saeed
                    new DateRange(new DateOnly(2025, 10, 1), new DateOnly(2025, 10, 3)),
                    "Sick leave"
                )
            );
            await db.SaveChangesAsync(cancellationToken);
        }

        // Seed UnitsManagers (many-to-many between OrgUnit and Employee)
        if (!db.Set<UnitsManagers>().Any())
        {
            var orgUnits = db.OrgUnits.ToList();
            var employees = db.Employees.ToList();
            var managers = new List<UnitsManagers>
            {
                // Main company
                new UnitsManagers(orgUnits[0].Id, employees[0].Id), // John manages HQ
                new UnitsManagers(orgUnits[1].Id, employees[1].Id), // Jane manages IT Dept
                new UnitsManagers(orgUnits[2].Id, employees[2].Id), // Ali manages HR Dept
                // Hayaricy company
                new UnitsManagers(orgUnits[3].Id, employees[3].Id), // Mohamed Zekry manages Hayaricy
                new UnitsManagers(orgUnits[4].Id, employees[4].Id), // Sara Ali manages Hayaricy IT
                new UnitsManagers(orgUnits[5].Id, employees[5].Id), // Omar Khaled manages Hayaricy HR
                new UnitsManagers(orgUnits[6].Id, employees[6].Id), // Lina Youssef manages Hayaricy Sales
                new UnitsManagers(orgUnits[7].Id, employees[7].Id), // Ahmed Samir manages Dev Team A
                new UnitsManagers(orgUnits[8].Id, employees[9].Id), // Karim Nabil manages Dev Team B
                new UnitsManagers(orgUnits[9].Id, employees[10].Id), // Mona Saeed manages Support Team
            };
            db.Set<UnitsManagers>().AddRange(managers);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
