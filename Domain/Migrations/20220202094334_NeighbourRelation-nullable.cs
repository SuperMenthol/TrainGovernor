using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class NeighbourRelationnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NeighbourRelationId",
                table: "LineStations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_NeighbourRelationId",
                table: "LineStations",
                column: "NeighbourRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineStations_NeighbouringStations_NeighbourRelationId",
                table: "LineStations",
                column: "NeighbourRelationId",
                principalTable: "NeighbouringStations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineStations_NeighbouringStations_NeighbourRelationId",
                table: "LineStations");

            migrationBuilder.DropIndex(
                name: "IX_LineStations_NeighbourRelationId",
                table: "LineStations");

            migrationBuilder.DropColumn(
                name: "NeighbourRelationId",
                table: "LineStations");

            migrationBuilder.AddColumn<int>(
                name: "NextStationId",
                table: "LineStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_NextStationId",
                table: "LineStations",
                column: "NextStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineStations_NeighbouringStations_NextStationId",
                table: "LineStations",
                column: "NextStationId",
                principalTable: "NeighbouringStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
