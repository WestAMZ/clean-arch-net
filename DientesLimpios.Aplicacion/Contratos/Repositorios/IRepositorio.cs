using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorio<T> where T : class
    {
        Task<T?> ObtenerPorId(Guid id);
        Task<IEnumerable<T>> ObtenerTodos();
        Task<T> Agregar(T entidad);
        Task<T> Actualizar(T entidad);
        Task<T> Borrar(T entidad);
    }
}
