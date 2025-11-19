using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportagController : ControllerBase
    {
        // CRUD
        [HttpGet("GetAll")]
        public IActionResult GetALl()
        {
            using (var context = new OlimpikonokContext())
            {

                try
                {
                    List<Sportag> sportagak = context.Sportags.ToList();
                    return Ok(sportagak);
                }
                catch (Exception ex)
                {
                    List<Sportag> valasz = new List<Sportag>();
                    Sportag hiba = new Sportag()
                    {
                        Id = -1,
                        Megnevezes = $"Hiba a betöltés közben: {ex.Message}"
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
                    Sportag valasz = context.Sportags.FirstOrDefault(o => o.Id == id);
                    if (valasz != null)
                    {
                        return Ok(valasz);

                    }
                    else
                    {
                        Sportag hiba = new Sportag() { Id = -1, Megnevezes = "Nincs ilyen azonosítójú sportág!" };

                        return StatusCode(404,hiba);
                    }
                }
                catch (Exception ex)
                {
                    Sportag hiba = new Sportag() { Id = -1, Megnevezes = $"Hiba az adatok betöltése során: {ex.Message}" };

                    return BadRequest(hiba);
                }
            }
        }




    }
}
