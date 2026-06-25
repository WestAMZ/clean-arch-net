using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CasoDeUsoCrearConsultorio : IRequestHandler<ComandoCrearConsultorio, Guid>
    {
        // Convención, tendremos un método llamado "Ejecutar" que va a recibir un comando y va a devolver un resultado

        private readonly IRepositorioConsultorios repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoCrearConsultorio(IRepositorioConsultorios repositorio, 
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Guid> Handle(ComandoCrearConsultorio comando) 
        {
            // orquestamos las acciones a realizar
            var consultorio = new Consultorio(comando.Nombre);

            try
            {
                var respuesta = await repositorio.Agregar(consultorio);
                await this.unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await this.unidadDeTrabajo.Reversar();
                throw;
            }
            
        }
    }
}
