using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class Correction_BUg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Produits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 1,
                column: "imageURL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 2,
                column: "imageURL",
                value: "");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_categorieId",
                table: "Produits",
                column: "categorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Categories_categorieId",
                table: "Produits",
                column: "categorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Categories_categorieId",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_categorieId",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Produits");
        }
    }
}
