using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedParticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Groups",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Challanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LocationId",
                table: "Groups",
                column: "LocationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Locations_LocationId",
                table: "Groups",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challanges_Locations_LocationId",
                table: "Challanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Locations_LocationId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_LocationId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Challanges_LocationId",
                table: "Challanges");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Groups");

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
    }
}
