using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class newVocabularIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vocabularies_WordId",
                table: "Vocabularies");

            migrationBuilder.AlterColumn<string>(
                name: "PartOfSpeech",
                table: "Vocabularies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_WordId_PartOfSpeech",
                table: "Vocabularies",
                columns: new[] { "WordId", "PartOfSpeech" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vocabularies_WordId_PartOfSpeech",
                table: "Vocabularies");

            migrationBuilder.AlterColumn<string>(
                name: "PartOfSpeech",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_WordId",
                table: "Vocabularies",
                column: "WordId");
        }
    }
}
