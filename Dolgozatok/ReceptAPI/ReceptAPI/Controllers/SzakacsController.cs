using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceptAPI.Models;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SzakacsController : ControllerBase
    {
        [HttpPut("Modosit")]
        public async Task<IActionResult> PutSzakacs(Szakac szakacs) 
        {
            using (var context = new ReceptdbContext()) 
            {
                try
                {
                  var modositando= context.Update(szakacs);
                    if (modositando != null)
                    {
                        await context.SaveChangesAsync();
                        return Ok("Sikeres módosítás");
                    }
                    return NotFound("Nem azonosítható szakács");


                }
                catch (Exception ex)
                {
                    return BadRequest("Hiba történt a módosításakor"+ex.Message);
                }
            }
        }

        [HttpDelete("Torol/{id}")]
        public async Task<IActionResult> DelSzakacs(int id) 
        {
            using (var context = new ReceptdbContext())
            {
                try
                {
                    var torlendo = await context.Szakacs.FindAsync(id);
                    if (torlendo != null)
                    {
                       context.Remove(torlendo);
                        return Ok("Sikeres törlés.");
                    }
                    return NotFound("Nem azonosítható szakács"+id);
                   


                }
                catch (Exception ex)
                {
                    return BadRequest("Hiba történt az adat törlésekor:" + ex.Message);
                }
            }
        }


    }
}
