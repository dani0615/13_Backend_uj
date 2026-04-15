using HalakAPI.DTOs;
using HalakAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HalakController : ControllerBase
    {
        [HttpGet("FajMeretTo")]
        public IActionResult GetKifogott()
        {
            using (var context = new HalakContext())
            {
                try
                {
                    var kifogott = context.Fogasoks.Select(f => new FajMeretDTO
                    {
                        Faj = f.Hal.Faj,
                        Meret = f.Hal.MeretCm,
                        ToNeve = f.Hal.To.Nev
                    }).ToList();
                    return Ok(kifogott);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost()]
        public IActionResult NewHal([FromBody] Halak hal)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    var UjHal = context.Halaks.Add(hal);
                    if (UjHal == null)
                    {
                        return BadRequest("Üres objektum nem rögzíthető!");
                    }
                    context.SaveChanges();
                    return Ok("Sikeres rögzítés.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut()]
        public async Task<IActionResult> PutHal(int id, [FromBody] Halak hal)
        {
            if (id != hal.Id)
            {
                return BadRequest("Az URL-ben megadott ID nem egyezik a kérés törzsében lévő ID-vel!");
            }

            using (var context = new HalakContext())
            {
                try
                {

                    var modositandoHal = await context.Halaks.FindAsync(id);

                    if (modositandoHal == null)
                    {
                        return NotFound($"Nincs ilyen azonosítójú hal!");
                    }

                    context.Entry(modositandoHal).CurrentValues.SetValues(hal);

                    await context.SaveChangesAsync();

                    return Ok("Sikeres módosítás!");
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpDelete()]
        public async Task<IActionResult> DeleteHal(int id)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    var halToDelete = context.Halaks.Find(id);
                    if (halToDelete == null)
                    {
                        return NotFound("Nincs ilyen azonosítóju hal!");
                    }
                    context.Halaks.Remove(halToDelete);
                    await context.SaveChangesAsync();
                    return Ok("Sikeres törlés!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

    }
}
