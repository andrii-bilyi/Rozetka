using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rozetka.Migrations
{
    /// <inheritdoc />
    public partial class ChildcategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_ChildcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories");

            migrationBuilder.RenameTable(
                name: "Subcategories",
                newName: "Childcategories");

            migrationBuilder.RenameIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Childcategories",
                newName: "IX_Childcategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Childcategories",
                table: "Childcategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Childcategories_Categories_CategoryId",
                table: "Childcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Childcategories_ChildcategoryId",
                table: "Products",
                column: "ChildcategoryId",
                principalTable: "Childcategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childcategories_Categories_CategoryId",
                table: "Childcategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Childcategories_ChildcategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Childcategories",
                table: "Childcategories");

            migrationBuilder.RenameTable(
                name: "Childcategories",
                newName: "Subcategories");

            migrationBuilder.RenameIndex(
                name: "IX_Childcategories_CategoryId",
                table: "Subcategories",
                newName: "IX_Subcategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_ChildcategoryId",
                table: "Products",
                column: "ChildcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
