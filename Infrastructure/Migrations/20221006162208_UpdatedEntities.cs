using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challanges_Locations_LocationId",
                table: "Challanges");

            migrationBuilder.DropIndex(
                name: "IX_Challanges_LocationId",
                table: "Challanges");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Challanges");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_LocationId",
                table: "Participants",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Locations_LocationId",
                table: "Participants",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Locations_LocationId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_LocationId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Participants");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Challanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Challanges_LocationId",
                table: "Challanges",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Challanges_Locations_LocationId",
                table: "Challanges",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
