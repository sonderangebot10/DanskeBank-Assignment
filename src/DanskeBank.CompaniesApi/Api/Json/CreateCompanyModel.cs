using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DanskeBank.CompaniesApi.Api.Json
{
    [JsonObject]
    public class CreateCompanyModel
    {
        [JsonProperty]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [JsonProperty]
        [Required(ErrorMessage = "Required")]
        public string Country { get; set; }
        [JsonProperty]
        [Required(ErrorMessage = "Required")]
        public string PhoneNumber { get; set; }
    }
}
