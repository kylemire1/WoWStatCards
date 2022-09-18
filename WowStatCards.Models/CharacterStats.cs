namespace WowStatCards.Models
{
    public class CharacterStats
    {
        public string CharacterName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? RenderUrl { get; set; }
        public int? Health { get; set; }
        public int? Strength { get; set; }
        public int? Agility { get; set; }
        public int? Intellect { get; set; }
        public int? Stamina { get; set; }
        public double? MeleeCrit { get; set; }
        public double? MeleeHaste { get; set; }
        public double? Mastery { get; set; }
        public int? BonusArmor { get; set; }
        public double? Lifesteal { get; set; }
        public double? Versatility { get; set; }
        public double? AttackPower { get; set; }
        public double? MainHandDamageMin { get; set; }
        public double? MainHandDamageMax { get; set; }
        public double? MainHandSpeed { get; set; }
        public double? MainHandDps { get; set; }
        public double? OffHandDamageMin { get; set; }
        public double? OffHandDamageMax { get; set; }
        public double? OffHandSpeed { get; set; }
        public double? OffHandDps { get; set; }
        public int? SpellPower { get; set; }
        public double? SpellCrit { get; set; }
        public int? Armor { get; set; }
    }
}
