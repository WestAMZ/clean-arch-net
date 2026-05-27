using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        public Paciente(string nombre, string email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }

            if (!email.Contains("@"))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} no es válido");
            }

            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }
    }
}
