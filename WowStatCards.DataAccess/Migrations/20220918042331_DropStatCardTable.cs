using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WowStatCards.DataAccess.Migrations
{
    public partial class DropStatCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatCards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: true),
                    Armor = table.Column<int>(type: "int", nullable: true),
                    AttackPower = table.Column<double>(type: "float", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BonusArmor = table.Column<int>(type: "int", nullable: true),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: true),
                    Intellect = table.Column<int>(type: "int", nullable: true),
                    Lifesteal = table.Column<double>(type: "float", nullable: true),
                    MainHandDamageMax = table.Column<double>(type: "float", nullable: true),
                    MainHandDamageMin = table.Column<double>(type: "float", nullable: true),
                    MainHandDps = table.Column<double>(type: "float", nullable: true),
                    MainHandSpeed = table.Column<double>(type: "float", nullable: true),
                    Mastery = table.Column<double>(type: "float", nullable: true),
                    MeleeCrit = table.Column<double>(type: "float", nullable: true),
                    MeleeHaste = table.Column<double>(type: "float", nullable: true),
                    OffHandDamageMax = table.Column<double>(type: "float", nullable: true),
                    OffHandDamageMin = table.Column<double>(type: "float", nullable: true),
                    OffHandDps = table.Column<double>(type: "float", nullable: true),
                    OffHandSpeed = table.Column<double>(type: "float", nullable: true),
                    RenderUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpellCrit = table.Column<double>(type: "float", nullable: true),
                    SpellPower = table.Column<int>(type: "int", nullable: true),
                    Stamina = table.Column<int>(type: "int", nullable: true),
                    Strength = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Versatility = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatCards_Factions_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatCards_FactionId",
                table: "StatCards",
                column: "FactionId");
        }
    }
}
