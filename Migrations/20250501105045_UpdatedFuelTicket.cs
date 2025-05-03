using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorViajes.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFuelTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelTickets_Trips_TripId",
                table: "FuelTickets");

            migrationBuilder.DropIndex(
                name: "IX_FuelTickets_TripId",
                table: "FuelTickets");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "FuelTickets");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "FuelTickets");

            migrationBuilder.AddColumn<long>(
                name: "FuelTicketId",
                table: "Trips",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FuelTickets",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_FuelTicketId",
                table: "Trips",
                column: "FuelTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_FuelTickets_FuelTicketId",
                table: "Trips",
                column: "FuelTicketId",
                principalTable: "FuelTickets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_FuelTickets_FuelTicketId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_FuelTicketId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "FuelTicketId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FuelTickets");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "FuelTickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "TripId",
                table: "FuelTickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_FuelTickets_TripId",
                table: "FuelTickets",
                column: "TripId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FuelTickets_Trips_TripId",
                table: "FuelTickets",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
