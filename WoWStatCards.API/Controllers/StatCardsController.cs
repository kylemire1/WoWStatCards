using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WowStatCards.DataAccess.Repository.IRepository;
using WowStatCards.Models.Domain;
using WowStatCards.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WoWStatCards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatCardsController : ControllerBase
    {
        private readonly IStatCardRepository _dbStatCards;
        private readonly IMapper _mapper;

        public StatCardsController(IStatCardRepository dbStatCards, IMapper mapper)
        {
            _dbStatCards = dbStatCards;
            _mapper = mapper;
        }

        // GET: api/<StatCardsController>
        [HttpGet]
        public async Task<List<StatCardDto>> Get()
        {
            var statCards = await _dbStatCards.GetAllAsync();
            return _mapper.Map<List<StatCardDto>>(statCards);
        }

        // GET api/<StatCardsController>/5
        [HttpGet("{id}")]
        public async Task<StatCardDto> Get(int id)
        {
            var statCard = await _dbStatCards.GetAsync(sc => sc.Id == id);
            return _mapper.Map<StatCardDto>(statCard);
        }

        // POST api/<StatCardsController>
        [HttpPost]
        public async Task<StatCardDto> Post([FromBody] StatCardDto statCardDto)
        {
            try
            {
                var statCard = _mapper.Map<StatCard>(statCardDto);
                statCard.UpdatedDate = DateTime.Now;
                statCard.CreatedDate = DateTime.Now;

                await _dbStatCards.CreateAsync(statCard);

                return _mapper.Map<StatCardDto>(statCard);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<StatCardsController>/5
        [HttpPut("{id}")]
        public async Task<StatCardDto> Put(int id, [FromBody] StatCardDto statCardDto)
        {

            var statCard = _mapper.Map<StatCard>(statCardDto);
            statCard.Id = id;
            statCard.UpdatedDate = DateTime.Now;

            await _dbStatCards.UpdateAsync(statCard);

            return _mapper.Map<StatCardDto>(statCard);
        }

        // DELETE api/<StatCardsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var statCard = await _dbStatCards.GetAsync(sc => sc.Id == id);
            await _dbStatCards.RemoveAsync(statCard);
        }

    }
}
