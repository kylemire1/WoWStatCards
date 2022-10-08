using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WowStatCards.Clients;
using WowStatCards.Models;
using WowStatCards.Models.Enum;
using WowStatCards.Models.View;

namespace WoWStatCards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterStatsController : ControllerBase
    {
        private readonly CharacterStatClient _statClient;
        private readonly BlizzAuthClient _blizzAuthClient;
        protected ApiResponse _response;

        public CharacterStatsController(CharacterStatClient statClient, BlizzAuthClient blizzAuthClient)
        {
            _statClient = statClient;
            _blizzAuthClient = blizzAuthClient;
            _response = new();
        }

        // GET: api/<CharacterStatsController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get(string characterName, string realm, string clientId)
        {
            try
            {
                var token = await _blizzAuthClient.RefreshLogin(clientId);
                var statData = await _statClient.GetStats(characterName, realm, token);
                var characterMediaData = await _statClient.GetCharacterMedia(characterName, realm, token);
                var characterProfile = await _statClient.GetCharacterProfile(characterName, realm, token);

                if (statData == null || characterMediaData == null || characterProfile == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                var avatarUrl = characterMediaData.Assets.FirstOrDefault(a => a.Key == "avatar")?.Value ?? "";
                var renderUrl = characterMediaData.Assets.FirstOrDefault(a => a.Key == "main-raw")?.Value ?? "";
                FactionEnum factionId = characterProfile.faction.name == "Alliance" ? FactionEnum.Alliance : FactionEnum.Horde;

                var statDto = new StatDto
                {
                    Health = statData.health,
                    Strength = statData.strength?.Effective,
                    Agility = statData.agility?.Effective,
                    Intellect = statData.intellect?.Effective,
                    Stamina = statData.stamina?.Effective,
                    MeleeCrit = statData.melee_crit?.value,
                    MeleeHaste = statData.melee_haste?.value,
                    Mastery = statData.mastery?.value,
                    BonusArmor = statData.bonus_armor,
                    Lifesteal = statData.lifesteal?.value,
                    Versatility = statData.versatility,
                    AttackPower = statData.attack_power,
                    MainHandDamageMax = statData.main_hand_damage_max,
                    MainHandDamageMin = statData.main_hand_damage_min,
                    MainHandDps = statData.main_hand_dps,
                    MainHandSpeed = statData.main_hand_speed,
                    OffHandDamageMax = statData.off_hand_damage_max,
                    OffHandDamageMin = statData.off_hand_damage_min,
                    OffHandDps = statData.off_hand_dps,
                    OffHandSpeed = statData.off_hand_speed,
                    SpellCrit = statData.spell_crit?.value,
                    SpellPower = statData.spell_power,
                    Armor = statData.armor?.Effective,
                    AvgItemLevel = characterProfile.average_item_level,
                    CharacterName = characterProfile.name,
                    AvatarUrl = avatarUrl,
                    RenderUrl = renderUrl,
                    FactionId = factionId,
                    Realm = characterProfile.realm.name
                };

                _response.Result = statDto;
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
    }
}
