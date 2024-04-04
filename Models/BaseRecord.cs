using System.Text.Json;
using System.Text.Json.Serialization;

namespace SeliniumApp.Models
{
    public record BaseRecord
    {
        // private static readonly JsonSerializerOptions _options =
        //new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        //public string ToJson()
        //{
        //    return JsonSerializer.Serialize(this,_options);
        //}
    }
}
