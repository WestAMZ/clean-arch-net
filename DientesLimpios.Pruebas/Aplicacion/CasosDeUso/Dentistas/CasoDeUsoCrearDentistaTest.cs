using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Dentistas
{
    [TestClass]
    public class CasoDeUsoCrearDentistaTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioDentistas repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearDentista casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Initialize() 
        {
            repositorio = Substitute.For<IRepositorioDentistas>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoCrearDentista(repositorio,unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoDatosValidos_CreaDentistaYPersisteYRetornaId() 
        {
            var comando = new ComandoCrearDentista
            {
                Nombre = "Dentista A",
                Email = "dentistaA@ejemplo.com"
            };

            var email = new Email(comando.Email);
            var dentistaCreado = new Dentista(comando.Nombre, email);
            var id = dentistaCreado.Id;

            repositorio.Agregar(Arg.Any<Dentista>()).Returns(dentistaCreado);

            var idResultado = await casoDeUso.Handle(comando);

            Assert.AreEqual(id, idResultado);
            await repositorio.Received(1).Agregar(Arg.Any<Dentista>());
            await unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExcepcion()
        {
            var comando = new ComandoCrearDentista
            {
                Nombre = "Dentista A",
                Email = "dentistaA@ejemplo.com"
            };

            repositorio.Agregar(Arg.Any<Dentista>())
                .Throws(new InvalidOperationException("Error al insertar"));
        }
    }
}
