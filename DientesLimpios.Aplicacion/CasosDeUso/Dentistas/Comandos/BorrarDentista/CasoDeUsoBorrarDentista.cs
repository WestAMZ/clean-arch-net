using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.BorrarDentista
{
    public class CasoDeUsoBorrarDentista : IRequestHandler<ComandoBorrarDentista>
    {
        private readonly IRepositorioDentistas repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoBorrarDentista(IRepositorioDentistas repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoBorrarDentista request)
        {
            var dentista = await repositorio.ObtenerPorId(request.Id);

            if ( dentista is null) 
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                await repositorio.Borrar(dentista);
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
