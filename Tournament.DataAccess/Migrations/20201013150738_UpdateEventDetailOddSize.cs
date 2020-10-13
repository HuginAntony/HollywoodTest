using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.DataAccess.Migrations
{
    public partial class UpdateEventDetailOddSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EventDetailOdd",
                table: "EventDetail",
                type: "decimal(18, 7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 7)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EventDetailOdd",
                table: "EventDetail",
                type: "decimal(38, 7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 7)");
        }
    }
}
