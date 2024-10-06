using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUser_AspNetUsers_AppUserId1",
                table: "BookUser");

            migrationBuilder.DropIndex(
                name: "IX_BookUser_AppUserId1",
                table: "BookUser");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "BookUser");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "BookUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Penalty",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_AppUserId",
                table: "BookUser",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUser_AspNetUsers_AppUserId",
                table: "BookUser",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUser_AspNetUsers_AppUserId",
                table: "BookUser");

            migrationBuilder.DropIndex(
                name: "IX_BookUser_AppUserId",
                table: "BookUser");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "BookUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "BookUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Penalty",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_AppUserId1",
                table: "BookUser",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUser_AspNetUsers_AppUserId1",
                table: "BookUser",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
