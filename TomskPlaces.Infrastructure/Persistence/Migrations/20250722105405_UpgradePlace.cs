using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TomskPlaces.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpgradePlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Caterings");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Places",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Caterings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
