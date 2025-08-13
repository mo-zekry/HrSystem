using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitsManagers_employees_EmployeeId",
                table: "UnitsManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitsManagers_org_units_OrgUnitId",
                table: "UnitsManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitsManagers",
                table: "UnitsManagers");

            migrationBuilder.DropIndex(
                name: "IX_UnitsManagers_OrgUnitId",
                table: "UnitsManagers");

            migrationBuilder.RenameTable(
                name: "UnitsManagers",
                newName: "units_managers");

            migrationBuilder.RenameIndex(
                name: "IX_UnitsManagers_EmployeeId",
                table: "units_managers",
                newName: "IX_units_managers_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_units_managers",
                table: "units_managers",
                columns: new[] { "OrgUnitId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_units_managers_employees_EmployeeId",
                table: "units_managers",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_units_managers_org_units_OrgUnitId",
                table: "units_managers",
                column: "OrgUnitId",
                principalTable: "org_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_units_managers_employees_EmployeeId",
                table: "units_managers");

            migrationBuilder.DropForeignKey(
                name: "FK_units_managers_org_units_OrgUnitId",
                table: "units_managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_units_managers",
                table: "units_managers");

            migrationBuilder.RenameTable(
                name: "units_managers",
                newName: "UnitsManagers");

            migrationBuilder.RenameIndex(
                name: "IX_units_managers_EmployeeId",
                table: "UnitsManagers",
                newName: "IX_UnitsManagers_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitsManagers",
                table: "UnitsManagers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UnitsManagers_OrgUnitId",
                table: "UnitsManagers",
                column: "OrgUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitsManagers_employees_EmployeeId",
                table: "UnitsManagers",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitsManagers_org_units_OrgUnitId",
                table: "UnitsManagers",
                column: "OrgUnitId",
                principalTable: "org_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
