using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trekstore.Migrations
{
    /// <inheritdoc />
    public partial class _4thUpdateDBPurchases10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Products_ProductsProductId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ProductsProductId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Purchases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProductsProductId",
                table: "Purchases",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Products_ProductsProductId",
                table: "Purchases",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
