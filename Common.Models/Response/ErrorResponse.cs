
using System.Text.Json.Serialization;

namespace Common.Models.Response;

public record ErrorResponse : BaseResponse
{
    public string type { get; set; }
    public string title { get; set; }
    public int status { get; set; }
    public Dictionary<string, string[]> errors { get; set; }

    [JsonIgnore]
    public string? GetError => errors?.FirstOrDefault().Value.FirstOrDefault();
}
