using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpikonokAPI.DTOs;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SportoloController : ControllerBase
    {
        // CRUD Műveletek

        [HttpGet]
        public IActionResult GetAll() 
        {
            using (var context = new OlimpikonokContext()) 
            {
                try
                {
                    var valasz = context.Sportolos.Include(s=> s.Orszag).Include(s=> s.Sportag).ToList();
                    return Ok(valasz);
                }
                catch (Exception ex )
                {
                    List<Sportolo> valasz = new List<Sportolo>();
                    Sportolo hiba = new Sportolo()
                    {
                        Id = -1,
                        Nev = $"Hiba a betöltés közben: {ex.Message}"
                    };
                    valasz.Add(hiba);
                    return BadRequest(valasz);
                }
            }
        }

        [HttpGet("SportoloOrszagSportagAll")]
        public IActionResult GetSportoloSOAll() 
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    List<Sportolo> sportolok = context.Sportolos.Include(s => s.Orszag).Include(s => s.Sportag).ToList();
                    List<SportoloSO> valasz = new List<SportoloSO>();
                    foreach(Sportolo sportolo in sportolok)
                    {
                        valasz.Add(new SportoloSO()
                        {
                            Id = sportolo.Id,
                            Nev = sportolo.Nev,
                            OrszagNev = sportolo.Orszag.Nev,
                            SportagNev = sportolo.Sportag.Megnevezes
                        });
                    }
                    return Ok(sportolok);
                }
                catch (Exception ex)
                {
                    List<Sportolo> valasz = new List<Sportolo>();
                    SportoloSO hiba = new SportoloSO()
                    {
                        Id = -1,
                        Nev = $"Hiba az adatok betöltése közben: {ex.Message}"
                    };
                    valasz.Add(hiba);
                    return BadRequest(valasz);
                    
                }
            }
        }

    }
}
