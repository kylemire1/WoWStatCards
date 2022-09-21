using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Authentication;
using WowStatCards.Models;

namespace WowStatCards.Clients
{
    public class BlizzAuthClient
    {
        private readonly RestClient _client;
        private readonly string _secret;

        public string? AccessToken { get; set; }

        public BlizzAuthClient(IConfiguration configuration)
        {
            _secret = configuration["AppSettings:Key"];
            _client = new RestClient("https://us.battle.net/oauth/token");
        }

        public async Task<string> RefreshLogin(string clientId)
        {
            var encodedBasicAuthToken = Base64EncodeSecret(clientId);
            var request = new RestRequest("", Method.Post);

            request.AddHeader("Authorization", "Basic " + encodedBasicAuthToken);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");

            try
            {
                var response = await _client.ExecuteAsync(request);
                var authData = JsonConvert.DeserializeObject<AuthResponse>(response.Content);
                AccessToken = authData.access_token;
                return authData.access_token;
            }
            catch (InvalidCredentialException ex)
            {

                throw ex;
            }
        }

        private string Base64EncodeSecret(string clientId)
        {
            var basicAuthToken = clientId + ":" + _secret;
            var basicAuthTokenBytes = System.Text.Encoding.UTF8.GetBytes(basicAuthToken);
            return Convert.ToBase64String(basicAuthTokenBytes);
        }
    }
}


