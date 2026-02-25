using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristaAPI.Models;

namespace TuristaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UtvonalController : ControllerBase
    {
        [HttpPost("uj")]
        public IActionResult PostUtvonal(Utvonal utvonal) 
        {
            using (var context = new TuristadbContext())
            {
                try
                {
                    context.Utvonals.Add(utvonal);
                    context.SaveChanges();
                    return Ok(new {Message="Sikeres mentés."});

                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a mentés során.{ex.Message}");

                }
            }
        }

        [HttpGet("All")]
        public IActionResult GetAll()
        {
            using (var context = new TuristadbContext())
            {
                try
                {
                    var utvonal = context.Utvonals.ToList();
                    return Ok(utvonal);               
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = $"Hiba a beolvasás közben: {ex.Message}"});
                   
                }
            }
        }

        [HttpGet("ById")]
        public IActionResult GetByID(int id) 
        {
            using (var context = new TuristadbContext())
            {
                try
                {
                    var keresett = context.Utvonals.Find(id);
                    if (keresett == null)
                    {
                        return StatusCode(419, new{ Message = "Valószínűleg nincs ilyen túra." });
                    } 
                    return Ok(keresett);
                }
                catch (Exception ex)
                {
                    return NotFound();
                   
                }
            }

        }

       




    }
}
