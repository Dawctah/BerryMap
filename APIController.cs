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
}
