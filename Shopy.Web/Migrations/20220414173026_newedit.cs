using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopy.Web.Migrations
{
    public partial class newedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "products_ibfk_1",
                table: "products");

            migrationBuilder.DropIndex(
                name: "vendorId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "category",
                table: "products");

            migrationBuilder.DropColumn(
                name: "vendorUsername",
                table: "products");

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "vendors",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "verificationCode",
                table: "vendors",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "rate",
                table: "products",
                type: "decimal(2,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldNullable: true,
                oldDefaultValueSql: "'0.0'");

            migrationBuilder.AddColumn<bool>(
                name: "IsSale",
                table: "models",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "models",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "vendorUsername",
                table: "models",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_models_vendorUsername",
                table: "models",
                column: "vendorUsername");

            migrationBuilder.AddForeignKey(
                name: "models_ibfk_1",
                table: "models",
                column: "vendorUsername",
                principalTable: "vendors",
                principalColumn: "username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "models_ibfk_1",
                table: "models");

            migrationBuilder.DropIndex(
                name: "IX_models_vendorUsername",
                table: "models");

            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "vendors");

            migrationBuilder.DropColumn(
                name: "verificationCode",
                table: "vendors");

            migrationBuilder.DropColumn(
                name: "IsSale",
                table: "models");

            migrationBuilder.DropColumn(
                name: "category",
                table: "models");

            migrationBuilder.DropColumn(
                name: "vendorUsername",
                table: "models");

            migrationBuilder.AlterColumn<decimal>(
                name: "rate",
                table: "products",
                type: "decimal(2,1)",
                nullable: true,
                defaultValueSql: "'0.0'",
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "products",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "vendorUsername",
                table: "products",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "vendorId",
                table: "products",
                column: "vendorUsername");

            migrationBuilder.AddForeignKey(
                name: "products_ibfk_1",
                table: "products",
                column: "vendorUsername",
                principalTable: "vendors",
                principalColumn: "username");
        }
    }
}
