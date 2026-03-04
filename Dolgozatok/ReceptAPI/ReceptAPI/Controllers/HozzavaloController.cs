using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceptAPI.Models;
using System.Net.Http.Headers;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HozzavaloController : ControllerBase
    {
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            using (var context = new ReceptdbContext())
            {
                try
                {
                    var hozzavalos = context.Hozzavalos.ToList();
                    return Ok(hozzavalos);

                }
                catch (Exception ex)
                {
                    return BadRequest("Hiba az adatok lekérése közben: " + ex.Message);
                }
            }
        }

        [HttpPost("Uj")]
        public async Task<IActionResult> UjHozzavalo(Hozzavalo hozzavalo) 
        {
            using (var context = new ReceptdbContext()) 
            {
                try
                {
                    await context.AddAsync(hozzavalo);
                    if (hozzavalo != null) 
                    {
                        await context.SaveChangesAsync();
                    }
                    return StatusCode(201, "Sikeres mentés");

                }
                catch (Exception ex)
                {
                    return BadRequest("Hiba történt az adat létrehozásakor"+ex.Message);
                    throw;
                }
            }
        }


    }
}
