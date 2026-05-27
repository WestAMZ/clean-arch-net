using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class CitaTest
    {
        private Guid _pacienteId = Guid.CreateVersion7();
        private Guid _dentistaId = Guid.CreateVersion7();
        private Guid _consultorioId = Guid.CreateVersion7();
        private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(
            DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

        [TestMethod]
        public void Constructor_CitaValida_EstadoEsProgramada()
        {
            // Act
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            // Assert
            Assert.AreEqual(_pacienteId, cita.PacienteId);
            Assert.AreEqual(_dentistaId, cita.DentistaId);
            Assert.AreEqual(_consultorioId, cita.ConsultorioId);
            Assert.AreEqual(_intervalo, cita.IntervaloDeTiempo);
            Assert.AreEqual(EstadoCita.Programada, cita.Estado);
            Assert.AreNotEqual(Guid.Empty, cita.Id);
        }

        [TestMethod]
        public void Constructor_FechaInicioEnElPasado_LanzarExcepcion()
        {
            Assert.ThrowsException<ExcepcionDeReglaDeNegocio>(() => new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1)));
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
        }

        [TestMethod]
        public void CancelarCita_EstadoNoProgramada_LanzaExcepcion()
        {
            // Arrange
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            // Act
            cita.Cancelar();
            // Assert
            Assert.ThrowsException<ExcepcionDeReglaDeNegocio>(() => cita.Cancelar());
        }

        [TestMethod]
        public void CancelarCita_EstadoProgramada_CambiaEstadoACancelada()
        {
            // Arrange
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            // Act
            cita.Cancelar();
            // Assert
            Assert.AreEqual(EstadoCita.Cancelada, cita.Estado);
        }
    }
}
