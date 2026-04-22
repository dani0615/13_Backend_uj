using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaktarWebAPI.Models;

namespace RaktarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetProducts(int page, int pageSize)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    var results = context.Termekeks.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    return Ok(results);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    var result = context.Termekeks.FirstOrDefault(t => t.Id == id);
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

        [HttpPost()]
        public IActionResult AddProduct(Termekek product)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    context.Termekeks.Add(product);
                    context.SaveChanges();
                    return Ok(product);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Termekek modositottTermek)
        {
            using (var context = new RaktarContext())
            {
                try
                {
                    var existingProduct = context.Termekeks.FirstOrDefault(t => t.Id == id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    context.Entry(existingProduct).CurrentValues.SetValues(modositottTermek);

                    context.SaveChanges();
                    return Ok(existingProduct);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) 
        {
            using (var context = new RaktarContext()) 
            {
                try
                {
                    var existingProduct = context.Termekeks.FirstOrDefault(t => t.Id == id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    context.Termekeks.Remove(existingProduct);
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
