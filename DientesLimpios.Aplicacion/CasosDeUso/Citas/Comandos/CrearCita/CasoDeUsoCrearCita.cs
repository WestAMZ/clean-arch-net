using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public class CasoDeUsoCrearCita : IRequestHandler<ComandoCrearCita, Guid>
    {
        private readonly IRepositorioCitas repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoCrearCita(IRepositorioCitas repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task<Guid> Handle(ComandoCrearCita request)
        {
            var citaSeSolapa = await repositorio
                .ExisteCitaSolapada(request.DentistaId, request.FechaInicio, request.FechaFin);

            if (citaSeSolapa) 
            {
                throw new ExcepcionDeValidacion("El dentista ya tiene una cita en ese horario");
            }

            var intervaloDeTiempo = new IntervaloDeTiempo(request.FechaInicio, request.FechaFin);
            var cita = new Cita(request.PacienteId, request.DentistaId, request.ConsultorioId, intervaloDeTiempo);

            try
            {
                var respuesta = await repositorio.Agregar(cita);
                await unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception) 
            {
                await unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
