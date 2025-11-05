using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace attendanceAPI.Migrations
{
    public partial class updateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileSize",
                table: "Image",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Image",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Image",
                newName: "FileSize");
        }
    }
}
