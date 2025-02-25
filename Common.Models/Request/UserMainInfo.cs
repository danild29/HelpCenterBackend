using Common.Models.Response;

namespace Common.Models.Request;

public record UserMainInfo : BaseResponse //, BaseRequest
{
    public string email { get; set; }
    public string password { get; set; }
}
