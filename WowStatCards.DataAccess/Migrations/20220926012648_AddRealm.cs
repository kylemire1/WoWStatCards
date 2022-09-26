using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WowStatCards.DataAccess.Migrations
{
    public partial class AddRealm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Realm",
                table: "StatCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Realm",
                table: "StatCards");
        }
    }
}
