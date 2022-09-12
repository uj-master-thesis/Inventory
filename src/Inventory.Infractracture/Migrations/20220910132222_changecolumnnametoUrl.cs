using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infractracture.Migrations
{
    public partial class changecolumnnametoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uri",
                table: "Posts",
                newName: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Posts",
                newName: "Uri");
        }
    }
}
