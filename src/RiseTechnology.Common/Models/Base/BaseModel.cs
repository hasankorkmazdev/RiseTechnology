using System;
using System.Text.Json.Serialization;

namespace RiseTechnology.Common.Models.Base
{
    public class BaseModel
    {
        [JsonPropertyOrder(1)]
        public Guid UUID { get; set; }
    }
}
