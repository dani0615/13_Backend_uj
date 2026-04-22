using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaktarWebAPI.Models;

namespace RaktarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetSuppliers(int page, int pageSize)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    var results = context.Beszallitoks.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    return Ok(results);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetSupplierById(int id)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    var result = context.Beszallitoks.FirstOrDefault(t => t.Id == id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
        }

        [HttpPost]
        public IActionResult AddSupplier(Beszallitok supplier)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    context.Beszallitoks.Add(supplier);
                    context.SaveChanges();
                    return Ok(supplier);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdatesSupplier(int id, Beszallitok modositottBesz)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    var modositando = context.Beszallitoks.FirstOrDefault(t => t.Id == id);
                    if (modositando == null)
                    {
                        return NotFound();
                    }
                    context.Entry(modositando).CurrentValues.SetValues(modositottBesz);
                    context.SaveChanges();
                    return Ok(modositando);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
        }


            
            [HttpDelete("{id}")]
            public IActionResult DeleteSupplier(int id)
            {
                using (var context = new RaktarContext())
                {
                    try
                    {
                        var torlendo = context.Beszallitoks.FirstOrDefault(t => t.Id == id);
                        if (torlendo == null)
                        {
                            return NotFound();
                        }
                        context.Beszallitoks.Remove(torlendo);
                        context.SaveChanges();
                        return Ok();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);

                    }
                }
            }

        }
    } 

