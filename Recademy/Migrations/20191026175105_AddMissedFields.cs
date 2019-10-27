using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recademy.Migrations
{
    public partial class AddMissedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "ReviewRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ReviewResponses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ReviewerId",
                table: "ReviewResponses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectSkills",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkills", x => new { x.SkillName, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectSkills_ProjectInfos_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Skills_SkillName",
                        column: x => x.SkillName,
                        principalTable: "Skills",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ReviewResponses");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "ReviewResponses");

            migrationBuilder.AddColumn<int>(
                name: "ReviewerId",
                table: "ReviewRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
