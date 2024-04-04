using System.Text.Json;
using System.Text.Json.Serialization;

namespace SeliniumApp.Models
{
    public record Article
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Content { get; set; }
        public string? Url { get; set; }
        public ICollection<string>? Tags { get; set; }


        public string ToJson()
        {
            return JsonSerializer.Serialize(this,
                new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        }
    }
}
