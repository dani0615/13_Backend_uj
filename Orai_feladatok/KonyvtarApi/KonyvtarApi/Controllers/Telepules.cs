using KonyvtarApi.DTOs;
using KonyvtarApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Telepules : ControllerBase
    {
        private readonly Models.KonyvtarakContext _context;

        public Telepules(Models.KonyvtarakContext context)
        {
            _context = context;
        }

     
        [HttpPost("InsertTelepules")]
        public async Task<IActionResult> UjTelepules (Telepulesek telepules) 
        {
            try
            {
               await _context.Telepuleseks.AddAsync(telepules);
               await _context.SaveChangesAsync();
                return Ok( new {Message="Sikeres rögzítés"} );

            }
            catch (Exception ex)
            {
                return BadRequest(new {Message=ex.Message});
               
            }

        }

        [HttpPut("ModositTelepules")]
        public async Task<IActionResult> PutTelepules(Telepulesek telepules)
        {
            try
            {
                _context.Telepuleseks.Update(telepules);
                await _context.SaveChangesAsync();
                if (telepules == null) 
                {
                    return NotFound(new { Message = "Nincs ilyen település" });
                }
                return Ok(new { Message = "Sikeres módosítás" });

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
             
            }
            
        }

        [HttpDelete("TorolTelepules/{id}")]
        public async Task<IActionResult> DelTelepules(int id)
        {
            try
            {
                var Telepules =  await _context.Telepuleseks.FindAsync(id);
                if (Telepules == null)
                {
                    return NotFound(new { Message = "Nincs ilyen település" });
                }


                _context.Telepuleseks.Remove(Telepules);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Sikeres törlés." });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpGet("Resznev/{reszlet}")]
        public async Task<IActionResult> GetByReszlet(string reszlet) 
        {
            try
            {
                var Telepulesek = await _context.Telepuleseks.Where(T => T.TelepNev.Contains(reszlet))
                    .Select(K => new TelepulesResznevDTO
                    {
                        TelepulesNev = K.TelepNev,
                        MegyeNeve = K.Megye.MegyeNev
                    }
                ).ToListAsync();
                if (Telepulesek.Count == 0)
                {
                    return NotFound(new { Message = "Nincs ilyen település"});
                }
                return Ok(Telepulesek);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
               
            }

        }



    }
}
