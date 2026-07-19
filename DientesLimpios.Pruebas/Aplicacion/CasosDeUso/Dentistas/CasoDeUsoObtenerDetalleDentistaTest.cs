using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerDetalleDentista;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Dentistas
{
    [TestClass]
    public class CasoDeUsoObtenerDetalleDentistaTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioDentistas repositorio;
        private CasoDeUsoObtenerDetalleDentista casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Initialize()
        {
            this.repositorio = Substitute.For<IRepositorioDentistas>();
            this.casoDeUso = new CasoDeUsoObtenerDetalleDentista(repositorio);
        }

        [TestMethod]
        public async Task Handle_DentistaExiste_RetornaDTO()
        {
            var email = new Email("dentistaA@ejemplo.com");
            var dentista = new Dentista("Dentista A", email);
            var id = dentista.Id;
            var consulta = new ConsultaObtenerDetalleDentista { Id = id };

            repositorio.ObtenerPorId(id).Returns(dentista);

            var resultado = await casoDeUso.Handle(consulta);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(id, resultado.Id);
            Assert.AreEqual(dentista.Nombre, resultado.Nombre);
            Assert.AreEqual(dentista.Email.Valor, resultado.Email);
        }

        [TestMethod]
        public async Task Handle_DentistaNoExiste_LanzaExcepcionNoEncontrado()
        {
            var id = Guid.NewGuid();
            var consulta = new ConsultaObtenerDetalleDentista { Id = id };

            repositorio.ObtenerPorId(id).ReturnsNull();

            await Assert.ThrowsExceptionAsync<ExcepcionNoEncontrado>(async () => 
            {
                var resultado = await casoDeUso.Handle(consulta);
            });
        }
    }
}
