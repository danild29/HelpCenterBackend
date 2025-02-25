namespace Common.Models.Request;

public record CreatePostRequest(string Title, string Content, Guid eventId) : BaseRequest;

public record CreateMessageRequest(string Content, Guid postId) : BaseRequest;