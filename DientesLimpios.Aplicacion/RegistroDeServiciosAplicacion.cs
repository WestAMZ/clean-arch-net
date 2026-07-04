using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion
{
    public static class RegistroDeServiciosAplicacion
    {
        public static IServiceCollection AgregarServiciosDeAplicacion( this IServiceCollection services) 
        {
            services.AddTransient<IMediator, MediadorSimple>();
            services.AddScoped<IRequestHandler<ComandoCrearConsultorio,Guid>, CasoDeUsoCrearConsultorio>();
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>, CasoDeUsoObtenerDetalleConsultorio>();
            services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>, CasoDeUsoObtenerListadoConsultorios>();
            services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, CasoDeUsoActualizarConsultorio>();
            services.AddScoped<IRequestHandler<ComandoBorrarConsultorio>, CasoDeUsoBorrarConsultorio>();

            return services;
        }
    }
}
