using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;

namespace CegautokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        private readonly FlottaContext _context;

        public RegistryController(FlottaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task <IActionResult> Registry(User user)
        {
            try
            {
                if (_context.Users.FirstOrDefault(u => u.LoginName == user.LoginName) != null)
                {
                    return BadRequest("Felhasználónév már foglalt.");
                }
                if (_context.Users.FirstOrDefault(u => u.Email == user.Email) != null)
                {
                    return BadRequest("Email cím már foglalt.");
                }
                user.Active = false;
                user.Permission = 1;
                user.Hash = Program.CreateSHA256(user.Hash);

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                Program.SendEmail(user.Email, "Regisztárció megerősítése", $"Kattintson a következő linkre a regisztráció megerősítéséhez: http://localhost:5222/Registry?felhasznalonev={user.LoginName}&email={user.Email}");
                return Ok("Sikeres regisztráció , erősítse meg a megadott emailre kiküldött linkre kattintva. ");
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba a regisztráció mentése közben: {ex.Message}");
            }
        }

        [HttpGet]

        public async Task <IActionResult> ConfirmRegistry(string felhasznalonev , string email)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u=> u.LoginName == felhasznalonev && u.Email == email);
                if (user != null)
                {
                    user.Active = true;
                    user.Permission = 2;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok("Sikeres regisztráció megerősítés.");
                }
                else 
                {
                    return BadRequest("Hibás adatok a regisztráció megerősítéséhez.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }

        }
    }
}
