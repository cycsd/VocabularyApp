using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addVocabularyNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefinitionId",
                table: "Defines");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IPA",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pronounce",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "IPA",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "Pronounce",
                table: "Vocabularies");

            migrationBuilder.AddColumn<int>(
                name: "DefinitionId",
                table: "Defines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
