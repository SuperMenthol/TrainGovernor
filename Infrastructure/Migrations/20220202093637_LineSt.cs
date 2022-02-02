using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class LineSt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "LineStations",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        LineId = table.Column<int>(type: "int", nullable: false),
        StationId = table.Column<int>(type: "int", nullable: false),
        NeighbourRelationId = table.Column<int>(type: "int", nullable: false),
        StationOrder = table.Column<int>(type: "int", nullable: false),
        BreakInMinutes = table.Column<int>(type: "int", nullable: false),
        AvgSpeed = table.Column<float>(type: "real", nullable: false),
        IsActive = table.Column<bool>(type: "bit", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_LineStations", x => x.Id);
        table.ForeignKey(
            name: "FK_LineStations_Lines_LineId",
            column: x => x.LineId,
            principalTable: "Lines",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        table.ForeignKey(
            name: "FK_LineStations_Stations_StationId",
            column: x => x.StationId,
            principalTable: "Stations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        table.ForeignKey(
            name: "FK_LineStations_Stations_NextStationId",
            column: x => x.NeighbourRelationId,
            principalTable: "NeighbouringStations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    });

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_LineId",
                table: "LineStations",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_StationId",
                table: "LineStations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_NeighbourId",
                table: "LineStations",
                column: "NeighbourRelationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
    name: "LineStations");
        }
    }
}
