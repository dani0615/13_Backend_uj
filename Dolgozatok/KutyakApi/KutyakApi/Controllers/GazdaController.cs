using KutyakApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KutyakApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GazdaController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Gazdum> gazda = new List<Gazdum>();
                using (var context = new KutyakContext())
                {
                    gazda = context.Gazda.ToList();
                }
                return Ok(gazda);

            }
            catch (Exception ex)
            {
                List<Gazdum> hiba = new List<Gazdum>();
                hiba.Add(new Gazdum() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);
            }


        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {

                using (var context = new KutyakContext()) 
                {
                    var gazda = context.Gazda.FirstOrDefault(g => g.Id == id);
                    if (gazda != null)
                    {
                        return Ok(gazda);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                List<Gazdum> hiba = new List<Gazdum>();
                hiba.Add(new Gazdum() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);
               
            }
            
        }


        [HttpPost("NewGazda")]
        public IActionResult NewGazda(Gazdum gazda) 
        {
            try
            {
                using (var context = new KutyakContext()) 
                {
                    context.Gazda.Add(gazda);
                    context.SaveChanges();
                    return Ok(gazda);
                }



            }
            catch (Exception ex)
            {
                List<Gazdum> hiba = new List<Gazdum>();
                hiba.Add(new Gazdum() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);
                
            }
        }

        [HttpPut("ModifyGazda")]
        public IActionResult ModifyGazda(Gazdum gazda)
        {
            try
            {
                using (var context = new KutyakContext())
                {
                    context.Gazda.Update(gazda);
                    context.SaveChanges();
                    return Ok(gazda);
                }
            }
            catch (Exception ex)
            {
                List<Gazdum> hiba = new List<Gazdum>();
                hiba.Add(new Gazdum() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);

            }
        }

        [HttpDelete("DeleteGazda/{id}")]
        public IActionResult DeleteGazda(int id) 
        {
           
                try
                {

                    using (var context = new KutyakContext())
                    {
                        var gazda = context.Gazda.FirstOrDefault(g => g.Id == id);
                        if (gazda != null)
                        {
                            context.Gazda.Remove(gazda);
                            context.SaveChanges();
                            return Ok(gazda);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
                catch (Exception ex)
                {
                    List<Gazdum> hiba = new List<Gazdum>();
                    hiba.Add(new Gazdum() { Id = -1, Nev = ex.Message, });
                    return BadRequest(hiba);

                }

            }
            
        }




    }

