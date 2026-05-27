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
    public class EmailTest
    {
        [TestMethod]
        public void Contructor_EmailNulo_LanzExcepcion()
        {
            Assert.ThrowsException<ExcepcionDeReglaDeNegocio>(() => new Email(null!));
        }

        [TestMethod]
        public void Contructor_EmailSinArroba_LanzExcepcion()
        {
            Assert.ThrowsException<ExcepcionDeReglaDeNegocio>(() => new Email("email.com"));
        }

        [TestMethod]
        public void Contructor_EmailValido_NoLanzaExcepcion()
        {
            var email = new Email("mail@test.com");
        }
    }
}
