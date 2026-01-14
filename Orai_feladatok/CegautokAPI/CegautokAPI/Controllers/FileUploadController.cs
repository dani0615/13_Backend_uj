using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        [HttpPost]
        [Route("ToFtpServer")]
        public async Task<IActionResult> FileUploadToFtp() 
        {
            try
            {

                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                Stream filestream = postedFile.OpenReadStream();
                string valasz = await Program.UploadToFtpServer(filestream, fileName);
                return Ok(valasz);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error:" + ex.Message);
                throw;
            }
        }
    }
}
