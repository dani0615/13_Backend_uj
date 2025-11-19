using CegautokAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("Users")]
        public IActionResult Users()
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    List<User> userek = context.Users.ToList();
                    return Ok(userek);
                }
                catch (Exception ex)
                {
                    List<User> hiba = new List<User>();
                    User hiba2 = new User()
                    {
                        Id = -1,
                        Name = $"Hiba a betöltés közben : {ex.Message}"
                    };
                    hiba.Add(hiba2);
                    return BadRequest(hiba);

                }

            }
        }

        [HttpGet("UserById/{id}")]
        public IActionResult UserById(int id)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    User valasz = context.Users.FirstOrDefault(u => u.Id == id);
                    if (valasz != null)
                    {
                        return Ok(valasz);
                    }
                    else
                    {
                        User hiba = new User()
                        {
                            Id = -1,
                            Name = $"Nincs ilyen ID-jú felhasználó: {id}"
                        };
                        return NotFound(hiba);
                    }

                }
                catch (Exception ex)
                {
                    User hiba2 = new User()
                    {
                        Id = -1,
                        Name = $"Hiba a betöltés közben : {ex.Message}"
                    };
                    return BadRequest(hiba2);

                }

            }

        }

        [HttpPost("NewUser")]
        public IActionResult NewUser(User user)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return Ok("Sikeres hozzáadás");

                }
                catch (Exception ex)
                {
                    User hiba2 = new User()
                    {
                        Id = -1,
                        Name = $"Hiba a hozzáadás közben : {ex.Message}"
                    };
                    return BadRequest(hiba2);
                    throw;
                }

            }

        }

        [HttpPut("ModifyUser")]
        public IActionResult ModifyUser(User user)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    context.Users.Update(user);
                    context.SaveChanges();
                    return Ok("Sikeres módosítás");



                }
                catch (Exception ex)
                {
                    User hiba2 = new User()
                    {
                        Id = -1,
                        Name = $"Hiba a módosítás közben : {ex.Message}"
                    };
                    return BadRequest(hiba2);
                }
            }

        }

        [HttpDelete("DelUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            using (var context = new FlottaContext())
            {
                try
                {
                    User user = context.Users.FirstOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        User hiba = new User()
                        {
                            Id = -1,
                            Name = $"Nincs ilyen ID-jú felhasználó: {id}"
                        };
                        return NotFound(hiba);
                    }
                    else 
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();
                        return Ok("Sikeres törlés");
                       
                    }
                    }
                catch (Exception ex)
                {
                    User hiba2 = new User()
                    {
                        Id = -1,
                        Name = $"Hiba a törlés közben : {ex.Message}"
                    };
                    return BadRequest(hiba2);
                }
            }

        }


    }
}

