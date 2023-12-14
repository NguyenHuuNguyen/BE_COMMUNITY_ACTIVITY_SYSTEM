using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Migrations
{
    public partial class UpdateRollAddRoleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Roles");
        }
    }
}
