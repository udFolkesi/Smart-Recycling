using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_CollectionPoint_CollectionPointID",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_User_UserID",
                table: "Operations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Operations",
                table: "Operations");

            migrationBuilder.RenameTable(
                name: "Operations",
                newName: "Operation");

            migrationBuilder.RenameIndex(
                name: "IX_Operations_UserID",
                table: "Operation",
                newName: "IX_Operation_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Operations_CollectionPointID",
                table: "Operation",
                newName: "IX_Operation_CollectionPointID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Operation",
                table: "Operation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_CollectionPoint_CollectionPointID",
                table: "Operation",
                column: "CollectionPointID",
                principalTable: "CollectionPoint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_User_UserID",
                table: "Operation",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_CollectionPoint_CollectionPointID",
                table: "Operation");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_User_UserID",
                table: "Operation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Operation",
                table: "Operation");

            migrationBuilder.RenameTable(
                name: "Operation",
                newName: "Operations");

            migrationBuilder.RenameIndex(
                name: "IX_Operation_UserID",
                table: "Operations",
                newName: "IX_Operations_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Operation_CollectionPointID",
                table: "Operations",
                newName: "IX_Operations_CollectionPointID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Operations",
                table: "Operations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_CollectionPoint_CollectionPointID",
                table: "Operations",
                column: "CollectionPointID",
                principalTable: "CollectionPoint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_User_UserID",
                table: "Operations",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
