namespace WowStatCards.Models.View
{
    public class StatResponse
    {
        public int? health { get; set; }
        public BaseEffective? strength { get; set; }
        public BaseEffective? agility { get; set; }
        public BaseEffective? intellect { get; set; }
        public BaseEffective? stamina { get; set; }
        public RatingBonusValue? melee_crit { get; set; }
        public RatingBonusValue? melee_haste { get; set; }
        public RatingBonusValue? mastery { get; set; }
        public int? bonus_armor { get; set; }
        public RatingBonusValue? lifesteal { get; set; }
        public double? versatility { get; set; }
        public double? attack_power { get; set; }
        public double? main_hand_damage_min { get; set; }
        public double? main_hand_damage_max { get; set; }
        public double? main_hand_speed { get; set; }
        public double? main_hand_dps { get; set; }
        public double? off_hand_damage_min { get; set; }
        public double? off_hand_damage_max { get; set; }
        public double? off_hand_speed { get; set; }
        public double? off_hand_dps { get; set; }
        public int? spell_power { get; set; }
        public RatingBonusValue? spell_crit { get; set; }
        public RatingBonusValue? spell_haste { get; set; }
        public RatingBonusValue? ranged_crit { get; set; }
        public RatingBonusValue? ranged_haste { get; set; }
        public BaseEffective? armor { get; set; }
    }
}