namespace NewsAgregator.WebAPI.Auth
{
    public class JwtAuthResult
    {
        public object AccessToken { get; set; }
        public RefreshToken RefreshToken{get;set;}
    }
}
