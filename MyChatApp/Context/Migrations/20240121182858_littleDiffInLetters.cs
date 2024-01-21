using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyChatApp.Migrations
{
    /// <inheritdoc />
    public partial class littleDiffInLetters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupId",
                table: "GroupUser");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupUser",
                newName: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupUser",
                newName: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupId",
                table: "GroupUser",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
