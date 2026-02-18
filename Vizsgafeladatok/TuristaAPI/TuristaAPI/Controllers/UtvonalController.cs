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



    }
}
