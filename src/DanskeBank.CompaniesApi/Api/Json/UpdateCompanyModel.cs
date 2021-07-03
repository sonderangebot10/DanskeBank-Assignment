using Newtonsoft.Json;

namespace DanskeBank.CompaniesApi.Api.Json
{
    [JsonObject]
    public class UpdateCompanyModel
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Country { get; set; }
        [JsonProperty]
        public string PhoneNumber { get; set; }
    }
}
