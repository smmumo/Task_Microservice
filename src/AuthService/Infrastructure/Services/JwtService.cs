
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AuthService.Interface;
using AuthService.Contracts;


namespace  AuthService.Infrastructure.Services;

public class JwtService(          
            IConfiguration iconfiguration
        ) : IJwtProvider
{
	private readonly IConfiguration _configuration = iconfiguration;  
	private readonly string JWTSettingsSecurityKey = "apiauthenticationSecretKey2024secretkey";
   
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
	{
		var Key = Encoding.UTF8.GetBytes(JWTSettingsSecurityKey);

		//todo,pass from configuration
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = false,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Key),
			ClockSkew = TimeSpan.Zero
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
		JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
		if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
		{
			throw new SecurityTokenException("Invalid token");
		}

		return principal;
	}    

    private static string GenerateRefreshToken()
	{
		var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public TokenResponse GenerateToken(string Email)
    {
       try
		{		
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(JWTSettingsSecurityKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
			  Subject = new ClaimsIdentity(
              [					
                new Claim(ClaimTypes.Email, Email),                
			  ]),
				Expires = DateTime.Now.AddMinutes(30),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
			
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var refreshToken = GenerateRefreshToken();
			return new TokenResponse (refreshToken,tokenHandler.WriteToken(token) );
		}
		catch (Exception)
		{
			return new TokenResponse("", "");
		}
    }

    public TokenResponse GenerateRefreshToken(string Email)
    {
         return GenerateToken(Email);
    }
}
