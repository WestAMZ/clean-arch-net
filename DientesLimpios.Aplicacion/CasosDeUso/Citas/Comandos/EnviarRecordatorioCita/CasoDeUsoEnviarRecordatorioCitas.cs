using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.EnviarRecordatorioCita
{
    public class CasoDeUsoEnviarRecordatorioCitas : IRequestHandler<ComandoEnviarRecordatorioCita>
    {
        private readonly IRepositorioCitas repositorio;
        private readonly IServicioNotificaciones ServicioNotificaciones;

        public CasoDeUsoEnviarRecordatorioCitas(IRepositorioCitas repositorio, IServicioNotificaciones servicioNotificaciones)
        {
            this.repositorio = repositorio;
            this.ServicioNotificaciones = servicioNotificaciones;
        }

        public async Task Handle(ComandoEnviarRecordatorioCita request)
        {
            var manana = DateTime.UtcNow.AddDays(1);
            var fechaInicio = manana;
            var fechaFin = manana.AddDays(1);
            var filtro = new FiltroCitasDTO 
            {
                FechaInicio = fechaInicio, 
                FechaFin = fechaFin, 
                EstadoCita = EstadoCita.Programada 
            };

            var citas = await repositorio.ObtenerFiltrado(filtro);
            foreach ( var cita in citas)
            {
                var citaDTO = cita.ADto();
                await ServicioNotificaciones.EnviarRecordatorioCita(citaDTO);
            }
        }
    }
}
