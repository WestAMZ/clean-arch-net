using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.Utilidades.Mediador
{
    [TestClass]
    public class MediadorSimpleTest
    {
        public class RequestFalso : IRequest<string> { }

        public class HandlerFalso : IRequestHandler<RequestFalso, string>
        {
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("Respuesta correcta");
            }
        }

        [TestMethod]
        public async Task Send_LlamaMetodoHandler() 
        {
            var request = new RequestFalso();

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider
                .GetService(typeof(IRequestHandler<RequestFalso, string>))
                .Returns(casoDeUsoMock);

            var mediador = new MediadorSimple(serviceProvider);

            var resultado = await mediador.Send(request);

            await casoDeUsoMock.Received(1).Handle(request);
        }

        [TestMethod]
        public async Task Send_SinHandlerRegistrado_LanzaExcepcion()
        {
            var request = new RequestFalso();

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();
            //serviceProvider
            //    .GetService(typeof(IRequestHandler<RequestFalso, string>))
            //    .Returns(casoDeUsoMock);

            var mediador = new MediadorSimple(serviceProvider);

            // Se produce excepción porque no se ha registrado un handler en el service provider
            await Assert.ThrowsExceptionAsync<ExcepcionDeMediador>( async() =>{
                var resultado = await mediador.Send(request);
            });

            //await casoDeUsoMock.Received(1).Handle(request);
        }
    }
}
