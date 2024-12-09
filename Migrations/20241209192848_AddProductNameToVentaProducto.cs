using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCategoryCrud.Migrations
{
    /// <inheritdoc />
    public partial class AddProductNameToVentaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "VentaProductos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "VentaProductos");
        }
    }
}
