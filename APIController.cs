using BerryMap.Models;
using BerryMap.Models.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace BerryMap
{
    public class APIController
    {
        private const string BaseURL = "https://pokeapi.co/api/v2/";

        private static HttpClient httpClient = new HttpClient();

        public static List<Berry> GetBerries()
        {
            try
            {
                List<BerryDTO> dto = new List<BerryDTO>();
                dto.Add(ParseResponse<BerryDTO>(SendRequest<string>("GET", BaseURL + "berry/")));

                int iterations = 0;
                while (dto[iterations].Next != null)
                {
                    dto.Add(ParseResponse<BerryDTO>(SendRequest<string>("GET", dto[iterations].Next)));
                    iterations++;
                }

                List<Berry> result = new List<Berry>();

                foreach (BerryDTO berryDTO in dto)
                {
                    foreach (Berry berry in berryDTO.Results)
                    {
                        result.Add(berry);
                    }
                }

                return result;
            }
            catch (HttpRequestException)
            {
                throw new System.NullReferenceException("Berries could not be found.");
            }
        }

        public static string GetPokemonName()
        {
            System.Random random = new System.Random();
            return ParseResponse<Pokemon>(SendRequest<string>("GET", BaseURL + "pokemon/" + random.Next(880))).Name;
        }

        public static string GetBerryIcon()
        {
            List<Berry> berries = GetBerries();
            System.Random random = new System.Random();

            Berry berry = ParseResponse<Berry>(SendRequest<string>("GET", BaseURL + "berry/" + random.Next(berries.Count)));
            berry.Item = ParseResponse<Item>(SendRequest<string>("GET", berry.Item.URL));

            return berry.Item.Sprite.Image;
        }

        private static T ParseResponse<T>(string res) => JsonConvert.DeserializeObject<T>(res);

        private static string SendRequest<T>(string method, string request)
        {
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(request).Result;

                HttpContent content = response.Content;
                response.EnsureSuccessStatusCode();
                string result = content.ReadAsStringAsync().Result;

                return result;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }

    public class Pokemon
    {
        private string name;

        [JsonProperty("name")]
        public string Name
        {
            get => Capitalize(name);
            set => name = value;
        }
        
        private string Capitalize(string input) => input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
    }
}
