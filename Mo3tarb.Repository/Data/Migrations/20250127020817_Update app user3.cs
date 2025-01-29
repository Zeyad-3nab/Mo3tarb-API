using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mo3tarb.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updateappuser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_Departments_DepartmentId",
                table: "AppUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AppUser");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "AppUser",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId1",
                table: "AppUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_DepartmentId1",
                table: "AppUser",
                column: "DepartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Department_DepartmentId",
                table: "AppUser",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Department_DepartmentId1",
                table: "AppUser",
                column: "DepartmentId1",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_Department_DepartmentId",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_Department_DepartmentId1",
                table: "AppUser");

            migrationBuilder.DropIndex(
                name: "IX_AppUser_DepartmentId1",
                table: "AppUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppUser");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "AppUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Departments_DepartmentId",
                table: "AppUser",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
