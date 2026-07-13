using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class CasoDeUsoObtenerListadoPacientes :
        IRequestHandler<ConsultaObtenerListadoPacientes, List<PacienteListadoDTO>>
    {
        private readonly IRepositorioPacientes repositorio;

        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<List<PacienteListadoDTO>> Handle(ConsultaObtenerListadoPacientes request)
        {
            var pacientes = await repositorio.ObtenerTodos();
            var pacientesDto = pacientes.Select(paciente => paciente.ADto()).ToList();
            return pacientesDto;
        }
    }
}
