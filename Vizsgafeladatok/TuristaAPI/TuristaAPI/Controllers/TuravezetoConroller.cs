using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristaAPI.Models;

namespace TuristaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TuravezetoConroller : ControllerBase
    {
        [HttpPut("modosit")]
        public IActionResult PutVezeto(Turavezeto turavezeto) 
        {
            using (var context = new TuristadbContext()) 
            {
                try
                {
                   
                    var vezeto = context.Turavezetos.Update(turavezeto);
                    if (turavezeto == null) 
                    {
                        return NotFound(new {Message="Nem azonosítható túravezető!"});
                    
                    }
                    context.SaveChanges();
                    return Ok(new {Message="Sikeres módosítás."});


                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a módosítás során.{ex.Message}");
                   
                }

            }
        }

        [HttpDelete("torol/{id}")]
        public IActionResult DeleteVezeto(int id)
        {
            using (var context = new TuristadbContext()) 
            {
                try
                {
                    var vezeto = context.Turavezetos.Find(id);
                    if (vezeto == null) 
                    {
                        return NotFound(new {Message="Nem azonosítható túravezető!"});
                    
                    }
                    context.Turavezetos.Remove(vezeto);
                    context.SaveChanges();
                    return Ok(new {Message="Sikeres törlés."});

                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a törlés során.{ex.Message}");
                   
                }
            }
        }

    }
}
