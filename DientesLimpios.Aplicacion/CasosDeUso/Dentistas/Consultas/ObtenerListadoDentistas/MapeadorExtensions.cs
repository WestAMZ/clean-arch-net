using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas
{
    public static class MapeadorExtensions
    {
        public static DentistaListadoDTO ADto(this Dentista dentistas)
        {
            var dto = new DentistaListadoDTO
            {
                Id = dentistas.Id,
                Nombre = dentistas.Nombre,
                Email = dentistas.Email.Valor,
            };

            return dto;
        }
    }
}
