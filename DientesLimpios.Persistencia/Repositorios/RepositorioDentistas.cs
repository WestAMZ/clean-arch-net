using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Persistencia.Utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioDentistas : Repositorio<Dentista>, IRepositorioDentistas
    {
        private readonly DientesLimpiosDbContext context;

        public RepositorioDentistas(DientesLimpiosDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Dentista>> ObtenerFiltrado(FiltroDentistaDTO filtro)
        {
            var querayable = context.Dentistas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                querayable = querayable.Where(x => x.Nombre.Contains(filtro.Nombre));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Email))
            {
                querayable = querayable.Where(x => x.Nombre.Contains(filtro.Email));
            }

            return await querayable.OrderBy(x => x.Nombre)
                .Paginar(filtro.Pagina, filtro.RegistrosPorPagina)
                .ToListAsync();
        }
    }
}
