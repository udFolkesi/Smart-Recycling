using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecyclingPointStatistics");

            migrationBuilder.AddColumn<int>(
                name: "ProcessingTrash",
                table: "RecyclingPoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CollectionPointStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Collected = table.Column<int>(type: "int", nullable: false),
                    Recycled = table.Column<int>(type: "int", nullable: false),
                    MostCollectedType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attendance = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectionPointID = table.Column<int>(type: "int", nullable: false),
                    RecyclingPointId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionPointStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionPointStatistics_CollectionPoint_CollectionPointID",
                        column: x => x.CollectionPointID,
                        principalTable: "CollectionPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionPointStatistics_RecyclingPoint_RecyclingPointId",
                        column: x => x.RecyclingPointId,
                        principalTable: "RecyclingPoint",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionPointStatistics_CollectionPointID",
                table: "CollectionPointStatistics",
                column: "CollectionPointID");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionPointStatistics_RecyclingPointId",
                table: "CollectionPointStatistics",
                column: "RecyclingPointId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionPointStatistics");

            migrationBuilder.DropColumn(
                name: "ProcessingTrash",
                table: "RecyclingPoint");

            migrationBuilder.CreateTable(
                name: "RecyclingPointStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecyclingPointID = table.Column<int>(type: "int", nullable: false),
                    Collected = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recycled = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclingPointStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecyclingPointStatistics_RecyclingPoint_RecyclingPointID",
                        column: x => x.RecyclingPointID,
                        principalTable: "RecyclingPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecyclingPointStatistics_RecyclingPointID",
                table: "RecyclingPointStatistics",
                column: "RecyclingPointID");
        }
    }
}
