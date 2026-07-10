using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacienteController : ControllerBase
    {
        private readonly IMediator mediator;

        public PacienteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDTO crearPacienteDTO) 
        {
            var comando = new ComandoCrearPaciente { Nombre = crearPacienteDTO.Nombre, Email = crearPacienteDTO.Email };
            await mediator.Send(comando);
            return Ok();
        }
    }
}
