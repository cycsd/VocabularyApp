using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Define_Vocabularies_VocabularyId",
                table: "Define");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Define",
                table: "Define");

            migrationBuilder.RenameTable(
                name: "Define",
                newName: "Defines");

            migrationBuilder.RenameIndex(
                name: "IX_Define_VocabularyId",
                table: "Defines",
                newName: "IX_Defines_VocabularyId");

            migrationBuilder.AlterColumn<int>(
                name: "VocabularyId",
                table: "Defines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Defines",
                table: "Defines",
                column: "DefineId");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    ReviewTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_WordId",
                table: "Cards",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defines_Vocabularies_VocabularyId",
                table: "Defines",
                column: "VocabularyId",
                principalTable: "Vocabularies",
                principalColumn: "VocabularyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defines_Vocabularies_VocabularyId",
                table: "Defines");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Defines",
                table: "Defines");

            migrationBuilder.RenameTable(
                name: "Defines",
                newName: "Define");

            migrationBuilder.RenameIndex(
                name: "IX_Defines_VocabularyId",
                table: "Define",
                newName: "IX_Define_VocabularyId");

            migrationBuilder.AlterColumn<int>(
                name: "VocabularyId",
                table: "Define",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Define",
                table: "Define",
                column: "DefineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Define_Vocabularies_VocabularyId",
                table: "Define",
                column: "VocabularyId",
                principalTable: "Vocabularies",
                principalColumn: "VocabularyId");
        }
    }
}
