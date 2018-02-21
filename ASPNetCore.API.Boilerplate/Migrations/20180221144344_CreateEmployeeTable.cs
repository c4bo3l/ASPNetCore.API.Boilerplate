using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore.API.Boilerplate.Migrations
{
    public partial class CreateEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeModel_Companies_CompanyModelCompanyID",
                table: "EmployeeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeModel",
                table: "EmployeeModel");

            migrationBuilder.RenameTable(
                name: "EmployeeModel",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeModel_CompanyModelCompanyID",
                table: "Employees",
                newName: "IX_Employees_CompanyModelCompanyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyModelCompanyID",
                table: "Employees",
                column: "CompanyModelCompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyModelCompanyID",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "EmployeeModel");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyModelCompanyID",
                table: "EmployeeModel",
                newName: "IX_EmployeeModel_CompanyModelCompanyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeModel",
                table: "EmployeeModel",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeModel_Companies_CompanyModelCompanyID",
                table: "EmployeeModel",
                column: "CompanyModelCompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
