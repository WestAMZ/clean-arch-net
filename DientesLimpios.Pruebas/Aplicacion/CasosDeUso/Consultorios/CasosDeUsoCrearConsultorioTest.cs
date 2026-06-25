using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasosDeUsoCrearConsultorioTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearConsultorio casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup() 
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();

            casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio() 
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };


            var consultorioCreado = new Consultorio("Consultorio A");
            repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            var resultado = await casoDeUso.Handle(comando);
            // Validamos que se recibió un llamado a validador con el comando
            // Validamos que se llamó el método agregar con un Consultorio
            // Validamos que se llamó el método Persistir de la unidad de trabajo
            // Validamos que el resultado no sea un Guid vacío
            await repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await unidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollBack() 
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
            repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await casoDeUso.Handle(comando);
            });

            // Validamos que se llamó el método Reversar de la unidad de trabajo
            await unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
