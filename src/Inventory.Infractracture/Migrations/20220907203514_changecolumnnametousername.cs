using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infractracture.Migrations
{
    public partial class changecolumnnametousername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Posts",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Comments",
                newName: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Posts",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Comments",
                newName: "Author");
        }
    }
}
