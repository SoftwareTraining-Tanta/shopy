using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopy.Web.Migrations
{
    public partial class addedRateToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "rate",
                table: "models",
                type: "decimal(2,1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rate",
                table: "models");
        }
    }
}
