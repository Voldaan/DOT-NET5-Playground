namespace MVC_Frontend_and_REST_API.Models.DataModels
{
    public class TokenGenerationResult
    {
        public string Token { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
