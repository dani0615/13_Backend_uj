
using CegautokAPI.DTOs;
using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        //végpont , getsalt , paraméterként felhasználónév (String) , és visszadja a tárolt salt-ot ha nincs akkor üres.
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
                    string doubleHash = Program.CreateSHA256(loginDTO.SentHash);
                    User user = context.Users.FirstOrDefault(u => u.LoginName == 
                    loginDTO.LoginName &&
                    u.Hash == doubleHash && 
                    u.Active );
                    if (user == null)
                       return NotFound("Nincs megfelelő felhasznló. A belépés sikertelen.");
                    return Ok("Sikeres bejelentkezés , küldöm a tokent ");

                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a bejelentekezés során: {ex.Message}");
                    throw;
                }
            }
        }



    }
}
