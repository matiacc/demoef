using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace formularios
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public Domicilio Domicilio { get; set; }
        public Persona()
        {
        }
    }
}
