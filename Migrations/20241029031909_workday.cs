using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApointementSystem.Migrations
{
    public partial class workday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "workDays");

            migrationBuilder.AddColumn<string>(
                name: "SelectedDays",
                table: "workDays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedDays",
                table: "workDays");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "workDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
