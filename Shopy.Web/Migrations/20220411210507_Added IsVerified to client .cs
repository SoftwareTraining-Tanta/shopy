using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopy.Web.Migrations
{
    public partial class AddedIsVerifiedtoclient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "clients");
        }
    }
}
