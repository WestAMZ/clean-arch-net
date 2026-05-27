using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Cita
    {
        public Guid Id { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid DentistaId { get; private set; }
        public Guid ConsultorioId { get; private set; }
        public Enums.EstadoCita Estado { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public Paciente? Paciente { get; private set; }
        public Dentista? Dentista { get; private set; }
        public Consultorio? Consultorio { get; private set; }

        public Cita(Guid pacienteId, Guid dentistaId, Guid consultorioId, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser mayor a la fecha fin");
            }

            if (fechaInicio < DateTime.UtcNow)
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser anterior a la fecha actual");
            }

            PacienteId = pacienteId;
            DentistaId = dentistaId;
            ConsultorioId = consultorioId;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Estado = EstadoCita.Programada;
            Id = Guid.CreateVersion7();
        }

        public void Cancelar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionDeReglaDeNegocio($"Solo se pueden cancelar citas programadas");
            }
            
            Estado = EstadoCita.Cancelada;
        }

        public void Completar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionDeReglaDeNegocio($"Solo se pueden completar citas programadas");
            }

            Estado = EstadoCita.Completada;
        }
    }
}
