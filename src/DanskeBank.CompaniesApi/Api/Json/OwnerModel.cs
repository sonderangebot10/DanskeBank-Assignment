using Newtonsoft.Json;

namespace DanskeBank.CompaniesApi.Api.Json
{
    [JsonObject]
    public class OwnerModel
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string SSN { get; set; }
    }
}
