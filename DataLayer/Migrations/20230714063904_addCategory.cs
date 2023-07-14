using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTags",
                columns: table => new
                {
                    CategoryTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTags", x => x.CategoryTagId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTagWord",
                columns: table => new
                {
                    CategoryTagsCategoryTagId = table.Column<int>(type: "int", nullable: false),
                    WordsWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTagWord", x => new { x.CategoryTagsCategoryTagId, x.WordsWordId });
                    table.ForeignKey(
                        name: "FK_CategoryTagWord_CategoryTags_CategoryTagsCategoryTagId",
                        column: x => x.CategoryTagsCategoryTagId,
                        principalTable: "CategoryTags",
                        principalColumn: "CategoryTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTagWord_Words_WordsWordId",
                        column: x => x.WordsWordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTagWord_WordsWordId",
                table: "CategoryTagWord",
                column: "WordsWordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTagWord");

            migrationBuilder.DropTable(
                name: "CategoryTags");
        }
    }
}
