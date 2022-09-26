namespace WowStatCards.Models
{
    public class ProfileResponse
    {
        public string name { get; set; }
        public TypeName faction { get; set; }
        public TypeName realm { get; set; }
        public int average_item_level { get; set; }
    }
}
