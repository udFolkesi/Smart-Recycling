using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Volume",
                table: "CollectionPoint");

            migrationBuilder.AddColumn<int>(
                name: "Fullness",
                table: "CollectionPointComposition",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullness",
                table: "CollectionPointComposition");

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "CollectionPoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
