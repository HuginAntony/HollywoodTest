using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.DataAccess.Migrations
{
    public partial class AddEventDetailSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventDetailStatus",
                columns: new[] { "EventDetailStatusId", "EventDetailStatusName" },
                values: new object[] { 1L, "Active" });

            migrationBuilder.InsertData(
                table: "EventDetailStatus",
                columns: new[] { "EventDetailStatusId", "EventDetailStatusName" },
                values: new object[] { 2L, "Scratched" });

            migrationBuilder.InsertData(
                table: "EventDetailStatus",
                columns: new[] { "EventDetailStatusId", "EventDetailStatusName" },
                values: new object[] { 3L, "Closed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventDetailStatus",
                keyColumn: "EventDetailStatusId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "EventDetailStatus",
                keyColumn: "EventDetailStatusId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "EventDetailStatus",
                keyColumn: "EventDetailStatusId",
                keyValue: 3L);
        }
    }
}
