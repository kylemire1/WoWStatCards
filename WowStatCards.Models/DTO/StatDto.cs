using WowStatCards.Models.Enum;

namespace WowStatCards.Models.View
{
    public class StatDto : CharacterStats
    {
        public string AvatarUrl { get; set; }
        public string RenderUrl { get; set; }
        public FactionEnum FactionId { get; set; }
        public int AvgItemLevel { get; set; }
    }
}