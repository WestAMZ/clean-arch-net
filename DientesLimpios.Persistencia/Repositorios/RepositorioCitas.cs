using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioCitas : Repositorio<Cita> , IRepositorioCitas
    {
        private readonly DientesLimpiosDbContext context;

        public RepositorioCitas(DientesLimpiosDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExisteCitaSolapada(Guid dentistaId, DateTime inicio, DateTime fin)
        {
            return await context.Citas
                .Where(
                    x => x.DentistaId == dentistaId && x.Estado == EstadoCita.Programada
                    && inicio < x.IntervaloDeTiempo.Fin && fin > x.IntervaloDeTiempo.Inicio
                )
                .AnyAsync();
        }

        // con new implementamos el new invocado en IRepositorioCitas
        new public async Task<Cita?> ObtenerPorId(Guid id) 
        {
            return await context.Citas
                .Include(x => x.Paciente)
                .Include(x => x.Dentista)
                .Include(x => x.Consultorio)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
