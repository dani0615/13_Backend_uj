using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceptAPI.DTOs;
using ReceptAPI.Models;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptController : ControllerBase
    {
        [HttpGet("ById{id}")]
        public IActionResult GetReceptById(int id)
        {
            using (var context = new ReceptdbContext()) 
            {
                try
                {
                    var keresett = context.Recepts.Find(id);
                    if (keresett != null)
                    {
                        var receptDTO = new ReceptDTO
                        {
                            Neve = keresett.Nev,
                            ElkeszitesiIdo = keresett.ElkeszitesiIdo,
                            NehezsegSzint = keresett.Nehezseg.Szint,
                            SzakacsNeve = keresett.Szakacs.Nev
                        };
                        return Ok(receptDTO);
                    }
                    else
                    {
                        return NotFound("Nincs receot a megadott ID-val" + id);
                    }
                   

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                    
                }
               
                
            }
        }
    }
}
