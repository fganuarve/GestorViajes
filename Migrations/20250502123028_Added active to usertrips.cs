using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorViajes.Migrations
{
    /// <inheritdoc />
    public partial class Addedactivetousertrips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "UserTrips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UserTrips");
        }
    }
}
