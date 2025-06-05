using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace StoreApi2.Token
{
    public class Token
    {
        public static object Create(IConfiguration configuration, string role)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Autentication:Key"]);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHanler = new JwtSecurityTokenHandler();
            var token = tokenHanler.CreateToken(tokenConfig);
            var tokenString = tokenHanler.WriteToken(token);

            return new
            {
                token = tokenString,
            };
        }

    }
}
