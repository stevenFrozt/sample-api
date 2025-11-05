using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace attendanceAPI.Migrations
{
    public partial class updateModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Image",
                newName: "Name");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImage",
                table: "User",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Image",
                newName: "FileName");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Image",
                type: "uuid",
                nullable: true);
        }
    }
}
