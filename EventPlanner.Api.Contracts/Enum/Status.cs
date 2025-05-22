using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.Enum
{
    // dit toont de waarde in plaats van 1, 2, 3..
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Status
    {
        todo = 1,
        doing = 2,
        done = 3,
        cancelled = 4,
    }
}
