using KonyvtarApi.DTOs;
using KonyvtarApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KonyvtarApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Konyvtar : ControllerBase
    {

        private readonly Models.KonyvtarakContext _context;

        public Konyvtar(Models.KonyvtarakContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var konyvtarak = _context.Konyvtaraks.ToList();
                return Ok(konyvtarak);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Hiba történt a könyvek lekérése során.", Error = ex.Message });

            }
        }


        [HttpDelete("Torol/{id}")]
        public IActionResult Torol(int id)
        {
            try
            {
                var konyvtar = _context.Konyvtaraks.Find(id);
                if (konyvtar == null)
                {
                    return NotFound();
                }
                _context.Konyvtaraks.Remove(konyvtar);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Hiba történt a könyv törlése során.", Error = ex.Message });

            }

        }


        [HttpPost("Uj")]
        public IActionResult Uj(Konyvtarak konyvtarak)
        {
            try
            {
                _context.Konyvtaraks.Add(konyvtarak);
                _context.SaveChanges();
                return Ok(new { Message = "Könyv sikeresen hozzáadva." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Hiba történt a könyv hozzáadása során.", Error = ex.Message });

            }
        }

        [HttpPut("Modosit")]
        public IActionResult Modosit(Konyvtarak konyvtarak)
        {
            try
            {
                _context.Konyvtaraks.Update(konyvtarak);
                _context.SaveChanges();
                return Ok(new { Message = "Könyv sikeresen módosítva." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Hiba történt a könyv módosítása során.", Error = ex.Message });

            }
        }


        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var konyv = _context.Konyvtaraks.Find(id);

                if (konyv == null)
                {
                    return NotFound(new { Message = "Ismeretlen könyvtár" });
                }
                return Ok(konyv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("Telepules{telepules}")]
        public IActionResult GetByTelepules(string telepules)
        {
            try
            {
                var konyvtarak = _context.Konyvtaraks.Where(k => k.IrszNavigation.TelepNev == telepules).ToList();

                if (konyvtarak.Count == 0)
                {
                    return NotFound(new { Message = $"Nincs ilyen település {telepules} vagy nincs könyvtár ott" });
                }
                return Ok(konyvtarak);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Hiba történt a könyvek lekérése során.", Error = ex.Message });

            }

        }

        [HttpGet("Megye{megye}")]
        public IActionResult GetByMegye(string megye)
        {
            try
            {
                var konyvtarak = _context.Konyvtaraks.Where(k => k.IrszNavigation.Megye.MegyeNev == megye)
                    .Select(k => new MegyeLekerdezDTO
                    {
                        id = k.Id,
                        telepulesNev = k.IrszNavigation.TelepNev,
                        konyvtarNev = k.KonyvtarNev
                    }).ToList();

                if (konyvtarak.Count == 0)
                {
                    return NotFound(new { Message = $"Nincs könyvtár a következő megyében: {megye} vagy nem létező megye." });
                }
                return Ok(konyvtarak);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        /*Készíts végpontot, amely egy könyvtár nevének részletéhez megadja, milyen településeken lehet az a könyvtár. A végpont url-je Telepules/KonyvtarNevreszlet/névrészlet ahol a névrészlet a könyvtár nevének egy részlete. Ha nincs megfelelő könyvtár, akkor a “Nincs megfelelő település” szöveggel térjen vissza 404-es státuszkóddal*/
        [HttpGet("KonyvtarNevreszlet{nevReszlet}")]
        public IActionResult GetByKonyvtarNevReszlet(string nevReszlet)
        {
            try
            {
                var telepulesek = _context.Konyvtaraks.Where(k => k.KonyvtarNev.Contains(nevReszlet))
                    .Select(k => k.IrszNavigation.TelepNev).Distinct().ToList();

                if (telepulesek.Count == 0)
                {
                    return NotFound(new { Message = $"Nincs megfelelő település a következő könyvtárnévrészlethez: {nevReszlet}." });
                }
                return Ok(telepulesek);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
    }
}
