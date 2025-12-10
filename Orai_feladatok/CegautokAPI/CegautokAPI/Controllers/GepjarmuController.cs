using CegautokAPI.DTOs;
using CegautokAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CegautokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GepjarmuController : ControllerBase
    {
        [Authorize]
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

        [HttpGet("{id}/Hasznalat")]
        public IActionResult GethasznalatById(int id)
        {
            using (var context = new FlottaContext())
                try
                {
                    List<JarmuHasznalatDTO> valasz = context.Kikuldottjarmus.Include(k => k.Kikuldetes)
                        .Include(k => k.Gepjarmu)
                        .Where(j => j.GepjarmuId == id).Select(j => new JarmuHasznalatDTO()
                        {
                            Id = id,
                            Rendszam = j.Gepjarmu.Rendszam,
                            Kezdes = j.Kikuldetes.Kezdes,
                            Befejezes = j.Kikuldetes.Befejezes
                        }).OrderBy(j => j.Kezdes).ToList();
                    return Ok(valasz);

                }
                catch (Exception)
                {
                    List<JarmuHasznalatDTO> valasz = new List<JarmuHasznalatDTO>() { new()
                    {
                        Id = -1,
                        Rendszam ="hiba" } };
                    return BadRequest(valasz);

                    throw;
                }        
        }


        [HttpGet("Sofor")]
        public IActionResult GetSofor() 
        {
            using (var context = new FlottaContext()) 
            {
                try
                {
                    List<SoforDTO> valasz = context.Kikuldottjarmus
                        .Include(j => j.Gepjarmu)
                        .Include(j => j.SoforNavigation)
                        .GroupBy(j => new { rsz = j.Gepjarmu.Rendszam, so = j.SoforNavigation.Name })
                        .Select(elem => new SoforDTO()
                        {
                            Rendszam = elem.Key.rsz,
                            SoforNev = elem.Key.so,
                            Darab = elem.Count()
                        })
                        .ToList();
                    return Ok(valasz);
                }
                catch (Exception)
                {
                    List<SoforDTO> valasz = new List<SoforDTO>()
                    {
                        new()
                        {
                            Rendszam = "hiba"
                        }
                    };
                    return BadRequest();
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
