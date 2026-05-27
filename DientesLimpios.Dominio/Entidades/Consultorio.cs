using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Consultorio
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
    }
}
