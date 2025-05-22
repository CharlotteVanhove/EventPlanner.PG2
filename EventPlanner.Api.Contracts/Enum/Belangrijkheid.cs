using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Belangrijkheid
    {

        must = 1,
        should = 2,
        could = 3,
    }
}
