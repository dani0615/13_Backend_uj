using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        // CRUD
        [HttpGet("GetAll")]
        public IActionResult GetALl() 
        {
            using (var context = new OlimpikonokContext())
            {

                try
                {
                    List<Orszag> orszagok = context.Orszags.ToList();
                    return Ok(orszagok);
                }
                catch (Exception ex)
                {
                    List<Orszag> valasz = new List<Orszag>();
                    Orszag hiba = new Orszag()
                    {
                        Id = -1,
                        Nev = $"Hiba a betöltés közben: {ex.Message}"
                    };
                    valasz.Add(hiba);
                    return BadRequest(valasz);
                    
                }
            }
            
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            using (var context = new OlimpikonokContext()) 
            {
                try
                {
                    Orszag valasz = context.Orszags.FirstOrDefault(o => o.Id == id);
                    if (valasz != null)
                    {
                        return Ok(valasz);

                    }
                    else {
                        Orszag hiba = new Orszag() { Id = -1, Nev = "Nincs ilyen azonosítójú ország!" };

                        return NotFound(hiba);
                    }
                }
                catch (Exception ex)
                {
                    Orszag hiba = new Orszag() { Id = -1, Nev = $"Hiba az adatok betöltése során: {ex.Message}" };

                    return BadRequest(hiba);
                }
            }
        }



    }
}
