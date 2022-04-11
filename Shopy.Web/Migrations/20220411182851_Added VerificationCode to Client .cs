using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopy.Web.Migrations
{
    public partial class AddedVerificationCodetoClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "verificationCode",
                table: "clients",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verificationCode",
                table: "clients");
        }
    }
}
