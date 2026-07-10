using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioPacientes : Repositorio<Paciente> , IRepositorioPacientes
    {
        public RepositorioPacientes(DientesLimpiosDbContext context)
            : base(context)
        {
            
        }
    }
}
