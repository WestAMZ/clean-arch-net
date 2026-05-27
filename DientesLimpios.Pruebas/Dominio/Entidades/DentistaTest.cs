using DientesLimpios.Dominio.Entidades;
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
    public class DentistaTest
    {
        [TestMethod]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            Email email = new Email("mail@test.com");
            Assert.ThrowsException<ExcepcionDeReglaDeNegocio>(() => new Dentista(null!, email));
        }

        [TestMethod]
        public void Consctructor_EmailNulo_LanzaExcepcion()
        {
            Email email = null!;
            Assert.ThrowsException<ExcepcionDeReglaDeNegocio>(() => new Dentista("Felipe", email));
        }
    }
}
