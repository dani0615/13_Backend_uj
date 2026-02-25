using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristaAPI.Models;

namespace TuristaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TuraController : ControllerBase
    {
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            using (var context = new TuristadbContext())
            {
                try
                {
                    var tura = context.Turas.ToList();
                    return Ok(tura);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                   
                }
            }
            
        }

        [HttpGet("By{ID}")]
        public IActionResult Get(int id) 
        {
            using (var context = new TuristadbContext())
            {
                try
                {
                    var tura = context.Turas.Where(t => t.Id == id).FirstOrDefault();
                    if (tura == null)
                    {
                        return NotFound(new {Message="Hiányzó túra!"});
                    }
                    return Ok(tura);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
        }

        [HttpPut("Modosit")]
        public async Task<IActionResult> PutTura(Tura tura) 
        {
            using (var context = new TuristadbContext()) 
            {
                try
                {
                    var modositando = context.Turas.Update(tura);
                    if(modositando == null) 
                    {
                        return NotFound("Hiányzó túra!");
                    }
                    await context.SaveChangesAsync();
                    return Ok("Sikeres módosítás!");


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                    
                }
            }
        }


        
    }
}
