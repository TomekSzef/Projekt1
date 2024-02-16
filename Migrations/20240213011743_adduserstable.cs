using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class adduserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        UserID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
            //        LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
            //        NIP = table.Column<string>(type: "nvarchar(20)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.UserID);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("users");
        }
    }
}
