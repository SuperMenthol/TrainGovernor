using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class Neighbourr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "NeighbouringStations",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        StationId = table.Column<int>(type: "int", nullable: false),
        NeighbourID = table.Column<int>(type: "int", nullable: false),
        DistanceInKm = table.Column<decimal>(type: "decimal", nullable: false),
        IsActive = table.Column<bool>(type: "bit", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_NeighbouringStations", x => x.Id);
        table.ForeignKey(
            name: "FK_Neighbours_StationId",
            column: x => x.StationId,
            principalTable: "Stations",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction);
    }).ForeignKey(
            name: "FK_Neighbours_NeighbourId",
            column: x => x.NeighbourID,
            principalTable: "Stations",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
