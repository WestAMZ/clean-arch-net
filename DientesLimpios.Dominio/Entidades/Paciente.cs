using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
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
        public Email Email { get; private set; } = null!;

        // Constructor vacio requerido por EF, lo dejamos privado para que no sea accesible para su uso
        private Paciente()
        {
            
        }

        public Paciente(string nombre, Email email)
        {
            AplicarReglasDeNegocioNombre(nombre);
            AplicarReglasDeNegocioEmail(email);

            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }

        public void ActualizarNombre(string nombre) 
        {
            AplicarReglasDeNegocioNombre(nombre);
            this.Nombre = nombre;
        }

        public void AplicarReglasDeNegocioNombre(string nombre) 
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }
        }

        public void ActualizarEmail(Email email) 
        {
            AplicarReglasDeNegocioEmail(email);
            Email = email;
        }

        public void AplicarReglasDeNegocioEmail(Email email) 
        {
            if (email == null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }
        }
    }
}
