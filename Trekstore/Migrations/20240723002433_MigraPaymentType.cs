using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trekstore.Migrations
{
    /// <inheritdoc />
    public partial class MigraPaymentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoDePagoID",
                table: "SalesDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoDePagoID",
                table: "PurchaseDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoDePago",
                columns: table => new
                {
                    tipoPagoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoPago = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDePago", x => x.tipoPagoID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_TipoDePagoID",
                table: "SalesDetails",
                column: "TipoDePagoID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_TipoDePagoID",
                table: "PurchaseDetails",
                column: "TipoDePagoID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_TipoDePago_TipoDePagoID",
                table: "PurchaseDetails",
                column: "TipoDePagoID",
                principalTable: "TipoDePago",
                principalColumn: "tipoPagoID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_TipoDePago_TipoDePagoID",
                table: "SalesDetails",
                column: "TipoDePagoID",
                principalTable: "TipoDePago",
                principalColumn: "tipoPagoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_TipoDePago_TipoDePagoID",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_TipoDePago_TipoDePagoID",
                table: "SalesDetails");

            migrationBuilder.DropTable(
                name: "TipoDePago");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetails_TipoDePagoID",
                table: "SalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseDetails_TipoDePagoID",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "TipoDePagoID",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "TipoDePagoID",
                table: "PurchaseDetails");
        }
    }
}
