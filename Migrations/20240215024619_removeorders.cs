using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class removeorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrdersOrderID = table.Column<int>(type: "int", nullable: false),
                    SparePartsPartID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrdersOrderID, x.SparePartsPartID });
                    table.ForeignKey(
                        name: "FK_OrderItems_Order_OrdersOrderID",
                        column: x => x.OrdersOrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_SparePart_SparePartsPartID",
                        column: x => x.SparePartsPartID,
                        principalTable: "SparePart",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SparePartsPartID",
                table: "OrderItems",
                column: "SparePartsPartID");
        }
    }
}
