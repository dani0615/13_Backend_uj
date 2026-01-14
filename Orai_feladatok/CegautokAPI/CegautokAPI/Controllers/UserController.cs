using CegautokAPI.DTOs;
using CegautokAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Mysqlx;

namespace CegautokAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly FlottaContext _context;

        public UserController(FlottaContext context)
        {
            _context = context;
        }
        [Authorize(Roles ="10")]
        [HttpGet("Users")]
        public IActionResult Users()
        {
            
            {
                try
                {
                    List<User> userek = _context.Users.ToList();
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
            
            {
                try
                {
                    User valasz = _context.Users.FirstOrDefault(u => u.Id == id);
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
            
            {
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Ok("Sikeres hozzáadás");

                }
                catch (Exception ex)
                {
                   
                    return BadRequest($"Hiba a rögzítés közben : {ex.Message}");
                    throw;
                }

            }

        }

        [HttpPut("ModifyUser")]
        public IActionResult ModifyUser(User user)
        {
            
            {
                try
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
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
           
            {
                try
                {
                    User user = _context.Users.FirstOrDefault(u => u.Id == id);
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
                        _context.Users.Remove(user);
                        _context.SaveChanges();
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

        [HttpGet("Jarmuvek/{id}")]
        public IActionResult GetUserJarmuvek(int id) 
        {
            
            {
                try
                {
                    List<SoforGepjarmuDTO> valasz = _context.Kikuldottjarmus.Include(k => k.Kikuldetes)
                        .Include(k => k.Gepjarmu)
                        .Include(k => k.SoforNavigation)
                        .Where(k => k.SoforNavigation.Id == id)
                        .Select(k => new SoforGepjarmuDTO()
                        {
                            Id = id,
                            Name = k.SoforNavigation.Name,
                            Kezdes = k.Kikuldetes.Kezdes,
                            Rendszam = k.Gepjarmu.Rendszam
                        }).ToList();

                    return Ok(valasz);

                }
                catch (Exception ex)
                {

                    return StatusCode(500, $"Hiba a lekérdezés közben: {ex.Message}");
                    throw;
                }

            }
        }


    }
}

