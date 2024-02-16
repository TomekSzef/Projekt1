using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class readdorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "OrderSparePart",
                columns: table => new
                {
                    OrdersOrderID = table.Column<int>(type: "int", nullable: false),
                    SparePartsPartID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSparePart", x => new { x.OrdersOrderID, x.SparePartsPartID });
                    table.ForeignKey(
                        name: "FK_OrderSparePart_Orders_OrdersOrderID",
                        column: x => x.OrdersOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSparePart_SparePart_SparePartsPartID",
                        column: x => x.SparePartsPartID,
                        principalTable: "SparePart",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSparePart_SparePartsPartID",
                table: "OrderSparePart",
                column: "SparePartsPartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSparePart");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
