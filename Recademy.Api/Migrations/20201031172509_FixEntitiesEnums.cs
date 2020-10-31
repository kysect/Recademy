using Microsoft.EntityFrameworkCore.Migrations;

namespace Recademy.Api.Migrations
{
    public partial class FixEntitiesEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewConclusion",
                table: "ReviewResponses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewConclusion",
                table: "ReviewResponses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
