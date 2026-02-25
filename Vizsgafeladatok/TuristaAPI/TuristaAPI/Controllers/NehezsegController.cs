using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristaAPI.Models;

namespace TuristaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NehezsegController : ControllerBase
    {
        [HttpPost("Uj")]
        public async Task<IActionResult> PostUjNehezseg(Nehezseg nehezseg) 
        {
            using (var context = new TuristadbContext()) 
            {
                try
                {
                    await context.Nehezsegs.AddAsync(nehezseg);
                    if (context.Nehezsegs.Select(x => x.Jelzes).Contains(nehezseg.Jelzes)) 
                    {
                        return StatusCode(406, new { Message = $"Már létező jelzés:{nehezseg.Jelzes}"});
                    }
                    await context.SaveChangesAsync();
                    return Ok();


                }
                catch (Exception ex)
                {
                    return BadRequest("Hiba a rögzítés közben"+ex.Message);
                }
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DelNehezseg(int id) 
        {
            using (var context = new TuristadbContext())
            {
                try
                {
                    var keresett = await context.Nehezsegs.FindAsync(id);
                    if (keresett == null) 
                    {
                        return NotFound("Nem létező nehézségi fok.");
                    }
                    context.Nehezsegs.Remove(keresett);
                    await context.SaveChangesAsync();
                    return Ok("Sikeres törlés");

                }
                catch (Exception ex)
                {
                    return BadRequest();
                    
                }
            }

        }

        

    }
}
