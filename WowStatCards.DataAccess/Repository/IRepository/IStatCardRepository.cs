using WowStatCards.Models.Domain;

namespace WowStatCards.DataAccess.Repository.IRepository
{
    public interface IStatCardRepository : IRepository<StatCard>
    {
        Task<StatCard> UpdateAsync(StatCard entity);
    }
}
