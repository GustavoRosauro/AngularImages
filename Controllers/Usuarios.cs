using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnularImage.Controllers
{
    public class Usuarios
    {
        public Usuarios(int id, string foto)
        {
            this.Id = id;
            this.Foto = foto;
        }
        public int Id { get; set; }
        public string Foto { get; set; }
        public string Arquivo { get; set; }
    }
}
