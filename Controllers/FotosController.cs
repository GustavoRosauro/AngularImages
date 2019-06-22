using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AgnularImage.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FotosController : Controller
    {
        [HttpPost("[action]"), DisableRequestSizeLimit]
        public void EnviaFoto()
        {
            try
            {
                var file = Request.Form.Files[0];
                //string folderName = "Upload";
                var usuario = Request.Form["usuario"].ToString();
                if (file.Length > 0)
                {
                    string fullPath = $@"C:\Users\Acer\source\repos\AgnularImage\AgnularImage\Imagens\{usuario}.png";
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            }
    }
}
