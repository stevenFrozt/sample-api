using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace attendanceAPI.Migrations
{
    public partial class updateModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "User",
                newName: "ProfileImageId");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageURL",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageURL",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ProfileImageId",
                table: "User",
                newName: "ProfileImage");
        }
    }
}
