using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infractracture.Migrations
{
    public partial class changecolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comments",
                newName: "Content");
        }
    }
}
