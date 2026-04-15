using HalakAPI.DTOs;
using HalakAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HorgaszokController : ControllerBase
    {

        [HttpGet("All")]
        public IActionResult All()
        {
            using (var context = new HalakContext())
            {

                try
                {
                    var horgaszok = context.Horgaszoks.ToList();
                    return Ok(horgaszok);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        //GetById

        [HttpGet("ById/{id}")]
        public IActionResult ById(int id) 
        {
            using (var context = new HalakContext()) 
            {
                try
                {
                    var keresett = context.Horgaszoks.Find(id);
                    if (keresett == null) 
                    {
                        return NotFound("Nincs ilyen azonosítójú horgász!");
                    }
                    return Ok(keresett);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
        }


        
        }

           
    }

