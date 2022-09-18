using Newtonsoft.Json;
using RestSharp;
using WowStatCards.Models;
using WowStatCards.Models.View;

namespace WowStatCards.Clients
{
    public class CharacterStatClient
    {
        private readonly RestClient _client;

        public CharacterStatClient()
        {
            _client = new RestClient("https://us.api.blizzard.com/profile/wow/character/");
        }

        public async Task<StatResponse> GetStats(string characterName, string realm, string token)
        {
            return await GetAsync<StatResponse>(token, GetStatRequestUri(characterName, realm));
        }

        public async Task<CharacterMediaResponse> GetCharacterMedia(string characterName, string realm, string token)
        {
            return await GetAsync<CharacterMediaResponse>(token, GetMediaRequestUri(characterName, realm));
        }

        private async Task<T> GetAsync<T>(string token, string uri)
        {
            var request = new RestRequest(uri, Method.Get);

            request.AddHeader("Authorization", "Bearer " + token);
            try
            {
                var response = await _client.ExecuteAsync(request);
                var statData = JsonConvert.DeserializeObject<T>(response.Content);

                return statData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetStatRequestUri(string characterName, string realm)
        {
            return realm.ToLower() + "/" + characterName.ToLower() + "/statistics?locale=en_US&namespace=profile-us";
        }

        private string GetMediaRequestUri(string characterName, string realm)
        {
            return realm.ToLower() + "/" + characterName.ToLower() + "/character-media?locale=en_US&namespace=profile-us";
        }
    }
}
