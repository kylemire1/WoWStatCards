namespace WowStatCards.Models.View
{
    public class StatDto : CharacterStats
    {
        public string CharacterName { get; set; }
        public string AvatarUrl { get; set; }
        public string RenderUrl { get; set; }
    }
}