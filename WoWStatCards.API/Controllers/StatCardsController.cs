using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WowStatCards.DataAccess.Repository.IRepository;
using WowStatCards.Models;
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
        protected ApiResponse _response;

        public StatCardsController(IStatCardRepository dbStatCards, IMapper mapper)
        {
            _dbStatCards = dbStatCards;
            _mapper = mapper;
            _response = new();
        }

        // GET: api/<StatCardsController>
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            try
            {
                var statCards = await _dbStatCards.GetAllAsync();

                _response.Result = _mapper.Map<List<StatCardDto>>(statCards);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        // GET api/<StatCardsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var statCard = await _dbStatCards.GetAsync(sc => sc.Id == id);

                if (statCard == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<StatCardDto>(statCard);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return _response;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        // POST api/<StatCardsController>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] StatCardDto statCardDto)
        {
            try
            {
                if (statCardDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var statCard = _mapper.Map<StatCard>(statCardDto);

                await _dbStatCards.CreateAsync(statCard);
                await _dbStatCards.SaveAsync();

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<StatCardDto>(statCard);

                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        // PUT api/<StatCardsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] StatCardDto statCardDto)
        {
            if (statCardDto == null || id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var statCard = _mapper.Map<StatCard>(statCardDto);
                statCard.UpdatedDate = DateTime.Now;

                await _dbStatCards.UpdateAsync(statCard);
                await _dbStatCards.SaveAsync();

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<StatCardDto>(statCard);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        // DELETE api/<StatCardsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var statCard = await _dbStatCards.GetAsync(sc => sc.Id == id);

                if (statCard == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbStatCards.RemoveAsync(statCard);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

    }
}
