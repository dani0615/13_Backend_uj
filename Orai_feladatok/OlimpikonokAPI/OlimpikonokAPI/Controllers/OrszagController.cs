using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        // CRUD
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetALl() 
        {
            using (var context = new OlimpikonokContext())
            {

                try
                {
                    List<Orszag> orszagok = await context.Orszags.ToListAsync();
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

        [HttpPut("ModositOrszag")]

        public async Task<IActionResult> PutOrszag(Orszag orszag) 
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    context.Orszags.Update(orszag);
                    await context.SaveChangesAsync();
                    return Ok(orszag);
                }
                catch (Exception ex)
                {
                    Orszag hiba = new Orszag() { Id = -1, Nev = $"Hiba az ország módosítása során: {ex.Message}" };
                    return BadRequest(hiba);
                }
            }

        }




    }
}
