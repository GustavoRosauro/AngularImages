using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
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
        [HttpGet("[action]")]
        public List<Usuarios> GetFiles()
        {
            List<Usuarios> lista = new List<Usuarios>();
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Acer\source\repos\AgnularImage\AgnularImage\Imagens");
            for (int i = 0; i < dir.GetFiles().Length; i++)
            {
                Usuarios user = new Usuarios(
                    i + 1,
                    dir.GetFiles()[i].FullName
                    );
                lista.Add(user);
            }
            return lista;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> DowloadFile([FromQuery] string arquivo)
        {
            try
            {
                //string file = Path.Combine(
                //  Path.Combine(arquivo, "App_Data"), "teste.png");

                var memory = new MemoryStream();
                using (var stream = new FileStream(arquivo, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                return File(memory, GetContentType(arquivo), "teste.png");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
