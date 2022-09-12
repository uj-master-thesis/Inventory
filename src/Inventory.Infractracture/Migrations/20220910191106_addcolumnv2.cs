using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infractracture.Migrations
{
    public partial class addcolumnv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileCompressed",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileCompressed",
                table: "Posts");
        }
    }
}
