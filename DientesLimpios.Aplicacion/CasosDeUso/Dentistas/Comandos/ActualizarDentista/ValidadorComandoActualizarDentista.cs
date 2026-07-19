using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.ActualizarDentista
{
    public class ValidadorComandoActualizarDentista : AbstractValidator<ComandoActualizarDentista>
    {
        public ValidadorComandoActualizarDentista()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250)
                .WithMessage("La longitud del campo {Property} debe ser menor o igual a {MaxLength}");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(254)
                .WithMessage("La longitud del campo {Property} debe ser menor o igual a {MaxLength}")
                .EmailAddress().WithMessage("EL formato del email no es válido");
        }
    }
}
