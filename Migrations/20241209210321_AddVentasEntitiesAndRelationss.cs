using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCategoryCrud.Migrations
{
    /// <inheritdoc />
    public partial class AddVentasEntitiesAndRelationss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaProductos_Products_ProductId",
                table: "VentaProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Users_UserId",
                table: "Ventas");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaProductos_Products_ProductId",
                table: "VentaProductos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Users_UserId",
                table: "Ventas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaProductos_Products_ProductId",
                table: "VentaProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Users_UserId",
                table: "Ventas");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaProductos_Products_ProductId",
                table: "VentaProductos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Users_UserId",
                table: "Ventas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
