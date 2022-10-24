using WowStatCards.Models.Enum;

namespace WowStatCards.Models.DTO
{
    public class StatCardDto : CharacterStats
    {
        public int? Id { get; set; }
        public string CardName { get; set; }
        public string CharacterName { get; set; }
        public string AvatarUrl { get; set; }
        public string RenderUrl { get; set; }
        public string UserEmail { get; set; }
        public FactionEnum FactionId { get; set; }
    }
}
