using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace attendanceAPI.Migrations
{
    public partial class addednewimagemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageURL",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ProfileImageId",
                table: "User",
                newName: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ImageId",
                table: "User",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Image_ImageId",
                table: "User",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Image_ImageId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ImageId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "User",
                newName: "ProfileImageId");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageURL",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
