using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WowStatCards.DataAccess.Migrations
{
    public partial class InitStatCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: true),
                    Strength = table.Column<int>(type: "int", nullable: true),
                    Agility = table.Column<int>(type: "int", nullable: true),
                    Intellect = table.Column<int>(type: "int", nullable: true),
                    Stamina = table.Column<int>(type: "int", nullable: true),
                    MeleeCrit = table.Column<double>(type: "float", nullable: true),
                    MeleeHaste = table.Column<double>(type: "float", nullable: true),
                    Mastery = table.Column<double>(type: "float", nullable: true),
                    BonusArmor = table.Column<int>(type: "int", nullable: true),
                    Lifesteal = table.Column<double>(type: "float", nullable: true),
                    Versatility = table.Column<double>(type: "float", nullable: true),
                    AttackPower = table.Column<double>(type: "float", nullable: true),
                    MainHandDamageMin = table.Column<double>(type: "float", nullable: true),
                    MainHandDamageMax = table.Column<double>(type: "float", nullable: true),
                    MainHandSpeed = table.Column<double>(type: "float", nullable: true),
                    MainHandDps = table.Column<double>(type: "float", nullable: true),
                    OffHandDamageMin = table.Column<double>(type: "float", nullable: true),
                    OffHandDamageMax = table.Column<double>(type: "float", nullable: true),
                    OffHandSpeed = table.Column<double>(type: "float", nullable: true),
                    OffHandDps = table.Column<double>(type: "float", nullable: true),
                    SpellPower = table.Column<int>(type: "int", nullable: true),
                    SpellCrit = table.Column<double>(type: "float", nullable: true),
                    Armor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatCards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatCards");
        }
    }
}
