using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WowStatCards.Models.Domain
{
    public class StatCard : CharacterStats
    {
        [Key]
        public int Id { get; set; }
        public string CardName { get; set; }
        public int AvgItemLevel { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        [ForeignKey("Faction")]
        public int FactionId { get; set; }
        public Faction Faction { get; set; }
    }
}
