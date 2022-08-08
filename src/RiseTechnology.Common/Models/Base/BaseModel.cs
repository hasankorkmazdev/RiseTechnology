using System;
using System.Text.Json.Serialization;

namespace RiseTechnology.Common.Models.Base
{
    public class BaseModel
    {
        [JsonPropertyName("uuid")]
        public Guid UUID { get; set; }
    }
}
