using KutyakApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KutyakApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KutyaController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Kutya> kutya = new List<Kutya>();
                using (var context = new KutyakContext())
                {
                    kutya = context.Kutyas.ToList();
                }
                return Ok(kutya);

            }
            catch (Exception ex)
            {
                List<Kutya> hiba = new List<Kutya>();
                hiba.Add(new Kutya() { Id = -1, Nev = ex.Message, });
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
                    var kutya = context.Kutyas.FirstOrDefault(g => g.Id == id);
                    if (kutya != null)
                    {
                        return Ok(kutya);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                List<Kutya> hiba = new List<Kutya>();
                hiba.Add(new Kutya() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);

            }

        }

        [HttpPost("NewKutya")]
        public IActionResult NewKutya(Kutya kutya)
        {
            try
            {
                using (var context = new KutyakContext())
                {
                    context.Kutyas.Add(kutya);
                    context.SaveChanges();
                    return Ok(kutya);
                }



            }
            catch (Exception ex)
            {
                List<Kutya> hiba = new List<Kutya>();
                hiba.Add(new Kutya() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);

            }
        }

        [HttpPut("ModifyKutya")]
        public IActionResult ModifyKutya(Kutya kutya)
        {
            try
            {
                using (var context = new KutyakContext())
                {
                    context.Kutyas.Update(kutya);
                    context.SaveChanges();
                    return Ok(kutya);
                }
            }
            catch (Exception ex)
            {
                List<Kutya> hiba = new List<Kutya>();
                hiba.Add(new Kutya() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);

            }
        }

        [HttpDelete("DeleteKutya/{id}")]
        public IActionResult DeleteKutya(int id)
        {

            try
            {

                using (var context = new KutyakContext())
                {
                    var kutya = context.Kutyas.FirstOrDefault(g => g.Id == id);
                    if (kutya != null)
                    {
                        context.Kutyas.Remove(kutya);
                        context.SaveChanges();
                        return Ok(kutya);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                List<Kutya> hiba = new List<Kutya>();
                hiba.Add(new Kutya() { Id = -1, Nev = ex.Message, });
                return BadRequest(hiba);

            }

        }
    }
}
