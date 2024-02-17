using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class initialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "SpareParts",
            //    columns: table => new
            //    {
            //        PartID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PartName = table.Column<string>(type: "varchar(100)", nullable: false),
            //        VehicleModel = table.Column<string>(type: "varchar(50)", nullable: false),
            //        Description = table.Column<string>(type: "text", nullable: false),
            //        Price = table.Column<float>(type: "decimal(10,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SparePart", x => x.PartID);
            //    });
            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    PartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    VehicleModel = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => x.PartID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "SpareParts");
            migrationBuilder.DropTable(
                name: "SparePart");
        }
    }
}
