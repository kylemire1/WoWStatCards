using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WowStatCards.DataAccess.Migrations
{
    public partial class AddCharacterNameAndRenders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "StatCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "StatCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FactionId",
                table: "StatCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RenderUrl",
                table: "StatCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Factions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatCards_FactionId",
                table: "StatCards",
                column: "FactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatCards_Factions_FactionId",
                table: "StatCards",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatCards_Factions_FactionId",
                table: "StatCards");

            migrationBuilder.DropTable(
                name: "Factions");

            migrationBuilder.DropIndex(
                name: "IX_StatCards_FactionId",
                table: "StatCards");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "StatCards");

            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "StatCards");

            migrationBuilder.DropColumn(
                name: "FactionId",
                table: "StatCards");

            migrationBuilder.DropColumn(
                name: "RenderUrl",
                table: "StatCards");
        }
    }
}
