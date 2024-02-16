using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class changedescriptiontype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SparePart",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SparePart",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
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
                oldType: "varchar(50)");
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SparePart",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }
    }
}
