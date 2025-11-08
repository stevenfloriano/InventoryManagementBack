using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [AllowAnonymous]
    public class JWTSecurityController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _handsShakeSecret;

        public JWTSecurityController(IConfiguration configuration)
        {
            _configuration = configuration;
            _handsShakeSecret = configuration.GetValue<string>("HandsShakeKey")!;
        }

        // POST: api/JWTSecurity
        /// <summary>
        /// Generate Token.
        /// </summary>
        /// <returns>Token created.</returns>
        /// <response code="200">Token record created.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        [HttpPost]
        public IActionResult GenerateToken([FromBody] PayloadRequestToken requestToken)
        {
            if (requestToken.SecretKey != _handsShakeSecret)
            {
                return BadRequest();
            }

            var jwtSettings = _configuration.GetSection("JWTSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey")!));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "coralUser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetValue<string>("Issuer"),
                audience: jwtSettings.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings.GetValue<string>("Expiration")!)),
                signingCredentials: credentials
            );

            var auxToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(auxToken);
        }

        // POST: api/JWTSecurity
        /// <summary>
        /// Refresh Token.
        /// </summary>
        /// <returns>Token Refreshed.</returns>
        /// <response code="200">Token record Refreshed.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        [HttpPost]
        public IActionResult RefreshToken([FromBody] PayloadRequestToken requestToken)
        {
            if (requestToken.SecretKey != _handsShakeSecret)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(requestToken.Token))
            {
                return BadRequest();
            }

            var jwtSettings = _configuration.GetSection("JWTSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey")!));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "coralUser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetValue<string>("Issuer"),
                audience: jwtSettings.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings.GetValue<string>("Expiration")!)),
                signingCredentials: credentials
            );

            var auxToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(auxToken);
        }
    }

    public class PayloadRequestToken
    {
        public string? Token { get; set; }

        public string? SecretKey { get; set; }

        public string? Audience { get; set; }
    }
}
