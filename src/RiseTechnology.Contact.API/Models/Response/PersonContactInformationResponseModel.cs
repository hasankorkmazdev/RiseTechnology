using RiseTechnology.Contact.API.Models.Base;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static RiseTechnology.Common.Enums;

namespace RiseTechnology.Contact.API.Models.Response
{
    public class PersonContactInformationResponseModel:BaseModel
    {
        [JsonPropertyOrder(2)]
        public string Name { get; set; }
        [JsonPropertyOrder(3)]
        public string LastName { get; set; }
        [JsonPropertyOrder(4)]
        public string Company { get; set; }
        [JsonPropertyOrder(5)]
        public bool IsActive { get; set; }
        [JsonPropertyOrder(6)]
        public List<ContactInfromationResponseModel> ContactInformations { get; set; }

    }
    public class ContactInfromationResponseModel:BaseModel
    {
        [JsonPropertyOrder(2)]
        public ContactType ContactType { get; set; }
        [JsonPropertyOrder(3)]
        public string ContactContent { get; set; }
    }
}
