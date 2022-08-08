using RiseTechnology.Common.Models.Base;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static RiseTechnology.Common.Enums;

namespace RiseTechnology.Common.Models.Response
{

    public class PersonContactInformationResponseModel : BaseModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("company")]
        public string Company { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("contactInformations")]
        public List<ContactInfromationResponseModel> ContactInformations { get; set; }

    }
    public class ContactInfromationResponseModel : BaseModel
    {
        [JsonPropertyName("contactType")]
        public ContactType ContactType { get; set; }
        [JsonPropertyName("contactContent")]
        public string ContactContent { get; set; }
    }

}
