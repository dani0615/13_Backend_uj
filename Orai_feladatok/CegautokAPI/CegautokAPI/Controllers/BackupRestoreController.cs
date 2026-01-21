using CegautokAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace CegautokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BackupRestoreController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private readonly FlottaContext _context;

        public BackupRestoreController(IWebHostEnvironment env, FlottaContext context)
        {
            _env = env;
            _context = context;


        }

        [Route("Backup/{fileName}")]
        //[Authorize("admin")]

        public async Task<IActionResult> SQLBackup(string fileName)
        {
            string sqlDataSource = _context.Database.GetConnectionString();
            MySqlCommand sqlCommand  = new MySqlCommand();
            MySqlBackup backup = new MySqlBackup();
            using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource)) 
            {
                try
                {
                    sqlCommand.Connection = myConnection;
                    myConnection.Open();
                    var filePath = "SQLBackupRestore/" + fileName;
                    backup.ExportToFile(filePath);
                    myConnection.Close();
                    if (System.IO.File.Exists(filePath)) 
                    {
                        var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                        return File(bytes, "text/plain", Path.GetFileName(filePath));
                    }
                    else
                    {
                        string hibauzenet = "Hiba a mentés során, a fájl nem található!";
                        byte[] hiba = new byte[hibauzenet.Length];
                        for (int i = 0; i < hibauzenet.Length; i++)
                        {
                            hiba[i] = Convert.ToByte(hibauzenet[i]);
                        }
                        return File(hiba, "text/plain", "Error.txt");
                    }

                }
                catch (Exception ex)
                {
                    return new JsonResult(ex.Message);
                    
                }
            }
        }

        [Route("Restore")]
        [HttpPost]
        //[Authorize("admin")]


        public JsonResult SQLRestore()  
        {
            try
            {
                string sqlDataSource = _context.Database.GetConnectionString();
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var filePath = _env.ContentRootPath +"/SQLBackupRestore/"+fileName;
                using (var stream = new FileStream(filePath, FileMode.Create)) 
                {
                    postedFile.CopyTo(stream);
                }
                MySqlCommand command = new MySqlCommand();
                MySqlBackup restore = new MySqlBackup(command);
                using (MySqlConnection mySqlConnection = new MySqlConnection(sqlDataSource)) 
                {
                    try
                    {
                        command.Connection = mySqlConnection;
                        mySqlConnection.Open();
                        restore.ImportFromFile(fileName);
                        System.IO.File.Delete(fileName);
                        return new JsonResult("Sikeres visszaállítás");

                    }
                    catch (Exception)
                    {
                        return new JsonResult("Mentésfájl sikeresen feltöltve. Hiba a szerver elérése során!");
                        
                    }

                }

            }
            catch (Exception)
            {
                return new JsonResult("Hiba a mentésfájl feltöltése során!");
                
            }
        }     
               


    }
}
