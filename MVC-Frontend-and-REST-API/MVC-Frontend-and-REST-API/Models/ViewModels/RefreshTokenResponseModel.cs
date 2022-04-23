namespace MVC_Frontend_and_REST_API.Models.ViewModels
{
    public class RefreshTokenResponseModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public bool TerminateSession { get; set; }
    }
}
