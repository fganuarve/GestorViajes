using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorViajes.Migrations
{
    /// <inheritdoc />
    public partial class AddFuelTicketUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "FuelTickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_FuelTickets_UserId",
                table: "FuelTickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuelTickets_Users_UserId",
                table: "FuelTickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelTickets_Users_UserId",
                table: "FuelTickets");

            migrationBuilder.DropIndex(
                name: "IX_FuelTickets_UserId",
                table: "FuelTickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FuelTickets");
        }
    }
}
