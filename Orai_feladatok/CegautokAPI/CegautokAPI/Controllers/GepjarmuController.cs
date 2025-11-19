using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GepjarmuController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    var gepjarmuvek = context.Gepjarmus.ToList();
                    return Ok(gepjarmuvek);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a betöltés közben: {ex.Message}" });
                }
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetbyId(int id)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    var gepjarmu = context.Gepjarmus.FirstOrDefault(g => g.Id == id);
                    if (gepjarmu != null)
                    {
                        return Ok(gepjarmu);
                    }
                    else
                    {
                        return NotFound(new { Error = $"Nincs ilyen ID-jú gépjármű: {id}" });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a lekérdezés közben: {ex.Message}" });
                }
            }

        }

        [HttpPost("NewJarmu")]
        public IActionResult NewJarmu(Gepjarmu jarmu)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    context.Gepjarmus.Add(jarmu);
                    context.SaveChanges();
                    return Ok(new { Message = "Gépjármű sikeresen hozzáadva." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a gépjármű hozzáadásakor: {ex.Message}" });
                }
            }



        }

        [HttpPut("UpdateJarmu")]
        public IActionResult UpdateJarmu(Gepjarmu jarmu) 
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    context.Gepjarmus.Update(jarmu);
                    context.SaveChanges();
                    return Ok(new { Message = "Gépjármű sikeresen frissítve." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a gépjármű frissítésekor: {ex.Message}" });
                }
            }
        }

        [HttpDelete("DeleteJarmu/{id}")]
        public IActionResult DeleteJarmu(int id) 
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    var jarmu = context.Gepjarmus.FirstOrDefault(g => g.Id == id);
                    if (jarmu != null)
                    {
                        context.Gepjarmus.Remove(jarmu);
                        context.SaveChanges();
                        return Ok(new { Message = "Gépjármű sikeresen törölve." });
                    }
                    else
                    {
                        return NotFound(new { Error = $"Nincs ilyen ID-jú gépjármű: {id}" });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a gépjármű törlésekor: {ex.Message}" });
                }
            }
        }

    }
}
