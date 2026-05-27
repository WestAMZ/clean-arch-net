using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public record IntervaloDeTiempo
    {
        public DateTime Inicio { get; }
        public DateTime Fin { get; }

        public IntervaloDeTiempo(DateTime inicio, DateTime fin)
        {
            if (inicio > fin)
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser mayor a la fecha fin");
            }

            Inicio = inicio;
            Fin = fin;
        }
    }
}
