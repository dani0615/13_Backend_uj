using CegautokAPI.DTOs;
using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CegautokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KikuldetesController : ControllerBase
    {
        private readonly FlottaContext _context;
        public KikuldetesController(FlottaContext context)
        {
            _context = context;
        }

        [HttpGet("Jarmuvek")]

        public IActionResult GetAll() 
        {
            
            {
                try 
                {
                    List<KikuldJarmuDTO> valasz = _context.Kikuldottjarmus
                        .Include(x=> x.Kikuldetes)
                        .Include(x=> x.Gepjarmu)
                        .Select(x=> new KikuldJarmuDTO() 
                        { Cim = x.Kikuldetes.Cim 
                        , Datum = x.Kikuldetes.Kezdes,
                        Rendszam= x.Gepjarmu.Rendszam}).ToList();
                    return Ok (valasz);

                }
                catch (Exception ex) 
                {
                    List<KikuldJarmuDTO> valasz = new List<KikuldJarmuDTO>();
                    KikuldJarmuDTO hiba = new KikuldJarmuDTO()
                    {
                        Cim = $"Hiba a kérés teljesítése közben:{ex.Message}"
                    };
                    valasz.Add(hiba);
                    return BadRequest(valasz);
                }

            }


        }



        [HttpGet("GetKikuldetesek")]
        public IActionResult GetKikuldetes()
        {
            
            {
                try
                {
                    var kikuldetesek = _context.Kikuldetes.ToList();
                    return Ok(kikuldetesek);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = $"Hiba a betöltés közben: {ex.Message}" });
                }

            }
        }

        [HttpGet("KikuldteById/{Id}")]
        public IActionResult GetKikuldteById(int Id)
        {
            
            {
                try
                {
                    var kikuldte = _context.Kikuldetes.FirstOrDefault(u => u.Id == Id);
                    if (kikuldte is Kikuldte)
                    {
                        return Ok(kikuldte);
                    }
                    else
                    {
                        return BadRequest("Nincs ilyen id!");

                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(new Kikuldte()
                    {
                        Id = -1,
                        Celja = $"Hiba történt: {ex.Message}",
                        Cim = "Hiba",
                        Kezdes = DateTime.Now,
                        Befejezes = DateTime.Now
                    });
                }
            }
        }

        [HttpPost("NewKikuldte")]
        public IActionResult AddNewKikuldte(Kikuldte kikuldte)
        {
            
            {
                try
                {

                    _context.Add(kikuldte);
                    _context.SaveChanges();
                    return Ok("Sikeres rögzítés");

                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba történt a felvétel során: {ex.Message}");
                }
            }
        }


        //[HttpPut("UpdateKikuldetes")]
        //public IActionResult UpdateKikuldetes(Kikuldte kuldetes)
        //{
        //    using (var context = new FlottaContext())
        //    {
        //        try
        //        {
        //            context.Kikuldtes.Update(kuldetes);
        //            context.SaveChanges();
        //            return Ok(new { Message = "Kiküldetés sikeresen frissítve." });


        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest(new { Error = "Hiba a frissítés közben." });

        //        }
        //    }

        //}

        //[HttpDelete("DeleteKikuldetes/{id}")]
        //public IActionResult DeleteKikuldetes(int id)
        //{
        //    using (var context = new FlottaContext())
        //    {
        //        try
        //        {
        //            var kuldetes = context.Kikuldtes.FirstOrDefault(k => k.Id == id);
        //            if (kuldetes != null)
        //            {
        //                context.Kikuldtes.Remove(kuldetes);
        //                context.SaveChanges();
        //                return Ok(new { Message = "Kiküldetés sikeresen törölve." });
        //            }
        //            else
        //            {
        //                return NotFound(new { Error = $"Nincs ilyen ID-jú kiküldetés: {id}" });
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest(new { Error = "Hiba a törlés közben." });
        //        }
        //    }
        //}

        // adott Id-jú kiküldetésen kik vettek részt , listázni kell a nevét (sofőr)

        [HttpGet("SoforByKikuldetes/{id}")]
        public IActionResult SoforByKikuldetes(int id) 
        {
            
            {
                try
                {

                    var SoforNeve = _context.Kikuldottjarmus
                    .Include(j => j.Kikuldetes)
                    .Include(j => j.SoforNavigation)
                    .FirstOrDefault(k => k.Id == id);
                    if(SoforNeve != null) 
                    {
                        string sofor = SoforNeve.SoforNavigation.Name;
                        return Ok(sofor);
                    }
                    
                    else
                        return NotFound(new { Message = "Nincs ilyen id" });
                    

                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba : {ex.Message}");
                    throw;
                }

            }
        }

    }
}
