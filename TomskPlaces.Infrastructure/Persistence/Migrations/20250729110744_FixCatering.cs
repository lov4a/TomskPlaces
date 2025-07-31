using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TomskPlaces.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCatering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CateringType",
                table: "Caterings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CateringType",
                table: "Caterings");
        }
    }
}
