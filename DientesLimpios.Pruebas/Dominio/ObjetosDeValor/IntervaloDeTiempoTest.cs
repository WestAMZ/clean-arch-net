using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{
    [TestClass]
    public class IntervaloDeTiempoTest
    {
        [TestMethod]
        public void Contructor_FechaInicioPosteriorAFechaFin_LanzaExcepcion()
        {
            Assert.ThrowsException<ExcepcionDeReglaDeNegocio>(() => new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1)));
        }

        [TestMethod]
        public void Contructor_ParametrosCorrectos_noLanzaExcepcion()
        {
            var intervalo = new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30));
            Assert.IsNotNull(intervalo);
        }
    }
}
