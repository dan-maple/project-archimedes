using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApi.Migrations
{
    public partial class UpdateJob_Cost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Jobs",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Jobs");
        }
    }
}
