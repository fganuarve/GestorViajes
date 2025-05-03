using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorViajes.Migrations
{
    /// <inheritdoc />
    public partial class Imagetickettable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "FuelTickets");

            migrationBuilder.CreateTable(
                name: "FuelTicketImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelTicketId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTicketImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelTicketImage_FuelTickets_FuelTicketId",
                        column: x => x.FuelTicketId,
                        principalTable: "FuelTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelTicketImage_FuelTicketId",
                table: "FuelTicketImage",
                column: "FuelTicketId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelTicketImage");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "FuelTickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
