using Microsoft.AspNetCore.Mvc;
using WowStatCards.Clients;
using WowStatCards.Models;
using WowStatCards.Models.View;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WoWStatCards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterStatsController : ControllerBase
    {
        private readonly CharacterStatClient _statClient;
        private readonly BlizzAuthClient _blizzAuthClient;

        public CharacterStatsController(CharacterStatClient statClient, BlizzAuthClient blizzAuthClient)
        {
            _statClient = statClient;
            _blizzAuthClient = blizzAuthClient;
        }

        // GET: api/<CharacterStatsController>
        [HttpGet]
        public async Task<StatDto> Get(string characterName, string realm)
        {
            var token = await _blizzAuthClient.RefreshLogin();
            StatResponse statData = await _statClient.GetStats(characterName, realm, token);
            CharacterMediaResponse characterMediaData = await _statClient.GetCharacterMedia(characterName, realm, token);
            string avatarUrl = characterMediaData.Assets.FirstOrDefault(a => a.Key == "avatar").Value;
            string renderUrl = characterMediaData.Assets.FirstOrDefault(a => a.Key == "main-raw").Value;

            var statDto = new StatDto
            {
                Health = statData.health,
                Strength = statData.strength.Effective,
                Agility = statData.agility.Effective,
                Intellect = statData.intellect.Effective,
                Stamina = statData.stamina.Effective,
                MeleeCrit = statData.melee_crit.value,
                MeleeHaste = statData.melee_haste.value,
                Mastery = statData.mastery.value,
                BonusArmor = statData.bonus_armor,
                Lifesteal = statData.lifesteal.value,
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
                SpellCrit = statData.spell_crit.value,
                SpellPower = statData.spell_power,
                Armor = statData.armor.Effective,
                CharacterName = characterName,
                AvatarUrl = avatarUrl,
                RenderUrl = renderUrl,
            };

            return statDto;
        }
    }
}
