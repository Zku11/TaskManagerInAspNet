using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerInAspNet.Migrations
{
    /// <inheritdoc />
    public partial class UserTaskAddingUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "UserTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_CreatorUserId",
                table: "UserTasks",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_AspNetUsers_CreatorUserId",
                table: "UserTasks",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_AspNetUsers_CreatorUserId",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTasks_CreatorUserId",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "UserTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
