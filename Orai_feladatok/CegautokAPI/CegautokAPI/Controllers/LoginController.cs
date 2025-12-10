using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CegautokAPI.DTOs;
using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CegautokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Jwtsettings _jwtSettings;

        public LoginController(Jwtsettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        // végpont: getsalt – paraméterként felhasználónév (String), visszadja a tárolt salt-ot, ha nincs akkor NotFound
        [HttpGet("GetSalt")]
        public IActionResult GetSalt(string loginName)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    User user = context.Users.FirstOrDefault(u => u.LoginName == loginName);
                    if (user == null)
                    {
                        return NotFound("Nincs ilyen felhasználó.");
                    }
                    return Ok(user.Salt);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Azonosítatlan hiba : {ex.Message}");
                    throw;
                }
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    
                    User user = context.Users.FirstOrDefault(u => u.LoginName == loginDTO.LoginName && u.Active);

                    if (user == null)
                    {
                        return NotFound("Nincs megfelelő felhasználó. A belépés sikertelen.");
                    }

                   
                    string calculatedDoubleHash = Program.CreateSHA256(loginDTO.SentHash + user.Salt);

                    
                    if (user.Hash != calculatedDoubleHash)
                        if (user.Hash != calculatedDoubleHash)
                        {
                            return NotFound("Nincs megfelelő felhasználó. A belépés sikertelen.");
                        }

                   
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),      
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.LoginName),
                        new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                        new Claim("permission", user.Permission.ToString()),               
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                        signingCredentials: creds
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a bejelentkezés során: {ex.Message}");
                    throw; 
                }
            }
        }
    }
}