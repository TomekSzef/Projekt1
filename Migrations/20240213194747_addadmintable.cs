using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addadmintable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    UserAdminID = table.Column<int>(type: "int", nullable: false)
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Admins");
        }
    }
}
