using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas
{
    public class CasoDeUsoObtenerListadoDentistas
        : IRequestHandler<ConsultaObtenerListadoDentistas, PaginadoDTO<DentistaListadoDTO>>
    {
        private readonly IRepositorioDentistas repositorio;

        public CasoDeUsoObtenerListadoDentistas(IRepositorioDentistas repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<PaginadoDTO<DentistaListadoDTO>> Handle(ConsultaObtenerListadoDentistas request)
        {
            var dentistas = await repositorio.ObtenerFiltrado(request);
            var totalDentistas = await repositorio.ObtenerCantidadTotalRegistros();
            var dentistasDto = dentistas.Select( dentista => dentista.ADto()).ToList();

            var paginadoDTO = new PaginadoDTO<DentistaListadoDTO>
            {
                Elementos = dentistasDto,
                Total = totalDentistas,
            };

            return paginadoDTO;
        }
    }
}
