using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Migrations
{
    public partial class UpdateCommunityActivityNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "CommunityActivities",
                newName: "StudentNote");

            migrationBuilder.AddColumn<string>(
                name: "AdminNote",
                table: "CommunityActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassPresidentNote",
                table: "CommunityActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadTeacherNote",
                table: "CommunityActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MajorHeadNote",
                table: "CommunityActivities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminNote",
                table: "CommunityActivities");

            migrationBuilder.DropColumn(
                name: "ClassPresidentNote",
                table: "CommunityActivities");

            migrationBuilder.DropColumn(
                name: "HeadTeacherNote",
                table: "CommunityActivities");

            migrationBuilder.DropColumn(
                name: "MajorHeadNote",
                table: "CommunityActivities");

            migrationBuilder.RenameColumn(
                name: "StudentNote",
                table: "CommunityActivities",
                newName: "Note");
        }
    }
}
