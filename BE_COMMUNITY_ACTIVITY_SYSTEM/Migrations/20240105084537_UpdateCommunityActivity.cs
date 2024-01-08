using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Migrations
{
    public partial class UpdateCommunityActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityActivities_CommunityActivityTypes_ActivityTpeId",
                table: "CommunityActivities");

            migrationBuilder.RenameColumn(
                name: "ActivityTpeId",
                table: "CommunityActivities",
                newName: "ActivityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityActivities_ActivityTpeId",
                table: "CommunityActivities",
                newName: "IX_CommunityActivities_ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityActivities_CommunityActivityTypes_ActivityTypeId",
                table: "CommunityActivities",
                column: "ActivityTypeId",
                principalTable: "CommunityActivityTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityActivities_CommunityActivityTypes_ActivityTypeId",
                table: "CommunityActivities");

            migrationBuilder.RenameColumn(
                name: "ActivityTypeId",
                table: "CommunityActivities",
                newName: "ActivityTpeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityActivities_ActivityTypeId",
                table: "CommunityActivities",
                newName: "IX_CommunityActivities_ActivityTpeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityActivities_CommunityActivityTypes_ActivityTpeId",
                table: "CommunityActivities",
                column: "ActivityTpeId",
                principalTable: "CommunityActivityTypes",
                principalColumn: "Id");
        }
    }
}
