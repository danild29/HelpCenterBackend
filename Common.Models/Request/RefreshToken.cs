namespace Common.Models.Request;

public record RefreshTokenRequest: BaseRequest
{
    public string refreshToken { get; set; }
}
