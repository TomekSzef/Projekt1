using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class changevaluetypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SparePart",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SparePart",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "ntext");
        }
    }
}
