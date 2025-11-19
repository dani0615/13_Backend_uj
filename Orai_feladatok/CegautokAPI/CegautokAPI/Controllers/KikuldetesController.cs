using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KikuldetesController : ControllerBase
    {
        [HttpGet("GetKikuldetesek")]
        public IActionResult GetKikuldetes()
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    var kikuldetesek = context.Kikuldtes.ToList();
                    return Ok(kikuldetesek);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a betöltés közben: {ex.Message}" });
                }

            }
        }

        [HttpGet("GetKikuldetesById/{id}")]
        public IActionResult GetKikuldetesById(int id)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    var kikuldetes = context.Kikuldtes.FirstOrDefault(k => k.Id == id);
                    if (kikuldetes != null)
                    {
                        return Ok(kikuldetes);
                    }
                    else
                    {
                        return NotFound(new { Error = $"Nincs ilyen ID-jú kiküldetés: {id}" });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a lekérdezés közben: {ex.Message}" });
                }
            }

        }

        [HttpPost("NewKikuldetes")]
        public IActionResult NewKikuldetes(Kikuldte kikuldetes)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    context.Kikuldtes.Add(kikuldetes);
                    context.SaveChanges();
                    return Ok(new { Message = "Kiküldetés sikeresen hozzáadva." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a hozzáadás közben: {ex.Message}" });
                }
            }
        }

        [HttpPut("UpdateKikuldetes")]
        public IActionResult UpdateKikuldetes(Kikuldte kuldetes)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    context.Kikuldtes.Update(kuldetes);
                    context.SaveChanges();
                    return Ok(new { Message = "Kiküldetés sikeresen frissítve." });


                }
                catch (Exception)
                {
                    return BadRequest(new { Error = "Hiba a frissítés közben." });

                }
            }

        }

        [HttpDelete("DeleteKikuldetes/{id}")]
        public IActionResult DeleteKikuldetes(int id)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    var kuldetes = context.Kikuldtes.FirstOrDefault(k => k.Id == id);
                    if (kuldetes != null)
                    {
                        context.Kikuldtes.Remove(kuldetes);
                        context.SaveChanges();
                        return Ok(new { Message = "Kiküldetés sikeresen törölve." });
                    }
                    else
                    {
                        return NotFound(new { Error = $"Nincs ilyen ID-jú kiküldetés: {id}" });
                    }
                }
                catch (Exception)
                {
                    return BadRequest(new { Error = "Hiba a törlés közben." });
                }
            }
        }
    }
}
