using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.DataAccess.Migrations
{
    public partial class CreateEventTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventDetailStatus",
                columns: table => new
                {
                    EventDetailStatusId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDetailStatusName = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetailStatus", x => x.EventDetailStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    TournamentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentName = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.TournamentId);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_TournamentId = table.Column<long>(nullable: false),
                    EventName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    EventNumber = table.Column<short>(nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventEndDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    AutoClose = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Event_Tournament",
                        column: x => x.FK_TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventDetail",
                columns: table => new
                {
                    EventDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_EventId = table.Column<long>(nullable: false),
                    FK_EventDetailStatusId = table.Column<long>(nullable: false),
                    EventDetailName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    EventDetailNumber = table.Column<short>(nullable: false),
                    EventDetailOdd = table.Column<decimal>(type: "decimal(38, 7)", nullable: false),
                    FinishingPosition = table.Column<short>(nullable: false),
                    FirstTimer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetail", x => x.EventDetailId);
                    table.ForeignKey(
                        name: "FK_EventDetailStatusId",
                        column: x => x.FK_EventDetailStatusId,
                        principalTable: "EventDetailStatus",
                        principalColumn: "EventDetailStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_EventDetails",
                        column: x => x.FK_EventId,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_FK_TournamentId",
                table: "Event",
                column: "FK_TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetail_FK_EventDetailStatusId",
                table: "EventDetail",
                column: "FK_EventDetailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetail_FK_EventId",
                table: "EventDetail",
                column: "FK_EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventDetail");

            migrationBuilder.DropTable(
                name: "EventDetailStatus");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Tournament");
        }
    }
}
