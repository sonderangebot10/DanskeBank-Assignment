using Newtonsoft.Json;

namespace DanskeBank.CompaniesApi.Api.Json
{
    [JsonObject]
    public class SsnValidationResponseModel
    {
        [JsonProperty]
        public bool Valid { get; set; }
    }
}
