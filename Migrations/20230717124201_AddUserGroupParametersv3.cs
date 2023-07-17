using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    /// <inheritdoc />
    public partial class AddUserGroupParametersv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersGroup_Groups_GroupId",
                table: "UsersGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersGroup_Users_UserId",
                table: "UsersGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersGroup",
                table: "UsersGroup");

            migrationBuilder.RenameTable(
                name: "UsersGroup",
                newName: "UsersGroups");

            migrationBuilder.RenameIndex(
                name: "IX_UsersGroup_UserId",
                table: "UsersGroups",
                newName: "IX_UsersGroups_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersGroup_GroupId",
                table: "UsersGroups",
                newName: "IX_UsersGroups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersGroups",
                table: "UsersGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGroups_Groups_GroupId",
                table: "UsersGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGroups_Users_UserId",
                table: "UsersGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersGroups_Groups_GroupId",
                table: "UsersGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersGroups_Users_UserId",
                table: "UsersGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersGroups",
                table: "UsersGroups");

            migrationBuilder.RenameTable(
                name: "UsersGroups",
                newName: "UsersGroup");

            migrationBuilder.RenameIndex(
                name: "IX_UsersGroups_UserId",
                table: "UsersGroup",
                newName: "IX_UsersGroup_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersGroups_GroupId",
                table: "UsersGroup",
                newName: "IX_UsersGroup_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersGroup",
                table: "UsersGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGroup_Groups_GroupId",
                table: "UsersGroup",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGroup_Users_UserId",
                table: "UsersGroup",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
