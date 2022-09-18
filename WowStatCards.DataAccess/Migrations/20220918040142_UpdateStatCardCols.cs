using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WowStatCards.DataAccess.Migrations
{
    public partial class UpdateStatCardCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardName",
                table: "StatCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StatCards",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "StatCards",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardName",
                table: "StatCards");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StatCards");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "StatCards");
        }
    }
}
