using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HrSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class menyManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_org_units_employees_ManagerId",
                table: "org_units");

            migrationBuilder.DropIndex(
                name: "IX_org_units_ManagerId",
                table: "org_units");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "org_units");

            migrationBuilder.AddColumn<string>(
                name: "PositionArabic",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PositionEnglish",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UnitsManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrgUnitId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitsManagers_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitsManagers_org_units_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "org_units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitsManagers_EmployeeId",
                table: "UnitsManagers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitsManagers_OrgUnitId",
                table: "UnitsManagers",
                column: "OrgUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitsManagers");

            migrationBuilder.DropColumn(
                name: "PositionArabic",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "PositionEnglish",
                table: "employees");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "org_units",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_org_units_ManagerId",
                table: "org_units",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_org_units_employees_ManagerId",
                table: "org_units",
                column: "ManagerId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
