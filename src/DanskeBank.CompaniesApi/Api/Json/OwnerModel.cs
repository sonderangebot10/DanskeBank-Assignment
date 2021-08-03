using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DanskeBank.CompaniesApi.Api.Json
{
    [JsonObject]
    public class OwnerModel
    {
        [JsonProperty]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [JsonProperty, JsonRequired]
        [Required(ErrorMessage = "Required")]
        public string SSN { get; set; }
    }
}
