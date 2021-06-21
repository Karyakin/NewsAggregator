using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace NewsAgregator.WebAPI.Auth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        public readonly IConfiguration _configuration;

        public JwtAuthManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtAuthResult GenerateTokens(string login, Claim[] claims) 
        {
            //собираем токен
            var jwtToken = new JwtSecurityToken("GoodNesAggregator",//issuer
                "GoodNesAggregator",//audience
                claims,//клеймы
                expires: DateTime.Now.AddMinutes(30), //from config
                signingCredentials: new SigningCredentials(//указываем как собираемся шифровать наш ключ
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),//передаем секретный ключ
                    SecurityAlgorithms.HmacSha256Signature));// алгорит на основе которого собираемся шифровать
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = new RefreshToken()
            {
                Login = login,
                ExpireAt = DateTime.Now.AddHours(24), //на протяжении 24 все валидно
                Token = Guid.NewGuid().ToString("D")
            };

            return new JwtAuthResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
