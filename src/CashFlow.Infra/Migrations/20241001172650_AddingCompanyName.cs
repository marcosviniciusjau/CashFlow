using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlow.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddingCompanyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "ManagerName");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ManagerName",
                table: "Users",
                newName: "Name");
        }
    }
}
