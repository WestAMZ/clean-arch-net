using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoActualizarConsultorioTest
    {
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoActualizarConsultorio casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoActualizarConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_ActualizaNombreYPersiste() 
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Nuevo nombre"};

            repositorio.ObtenerPorId(id).Returns(consultorio);

            await casoDeUso.Handle(comando);

            await repositorio.Received(1).Actualizar(consultorio);
            await unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado() 
        {
            var comando = new ComandoActualizarConsultorio { Id = Guid.NewGuid(), Nombre = "Nombre"};
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            await Assert.ThrowsExceptionAsync<ExcepcionNoEncontrado>(async () => 
                await casoDeUso.Handle(comando));
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcionAlActualizar_LlamaAReversarYLanzaExcepcion() 
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Nuevo nombre"};

            repositorio.ObtenerPorId(id).Returns(consultorio);
            repositorio.Actualizar(consultorio).Throws(new Exception("Error al actualizar"));
            await Assert.ThrowsExceptionAsync<Exception>(async () => 
                await casoDeUso.Handle(comando));
            await unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
