using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Dentista
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public string Email { get; private set; } = null!;
    }
}
