using WowStatCards.DataAccess.Repository.IRepository;
using WowStatCards.Models.Domain;

namespace WowStatCards.DataAccess.Repository
{
    public class StatCardRepository : Repository<StatCard>, IStatCardRepository
    {
        private readonly DataContext _context;

        public StatCardRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StatCard> UpdateAsync(StatCard entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Update(entity);

            return entity;
        }
    }
}
