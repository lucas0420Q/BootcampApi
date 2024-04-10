using Core.Entities;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations
{
    public class CreateCreditCardValidation : AbstractValidator<CreateCreditCardModel>
    {
        public CreateCreditCardValidation()
        {
            RuleFor(x => x.Designation)
                .MaximumLength(50).WithMessage("La designación no puede tener más de 50 caracteres.");

            RuleFor(x => x.IssueDate)
                .NotEmpty().WithMessage("La fecha de emisión no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de emisión debe ser anterior o igual a la fecha actual.");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("La fecha de vencimiento no puede estar vacía.")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("La fecha de vencimiento debe ser posterior o igual a la fecha actual.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("El número de tarjeta no puede estar vacío.")
                .GreaterThan(0).WithMessage("El número de tarjeta debe ser mayor que 0.");

            RuleFor(x => x.CVV)
                .NotEmpty().WithMessage("El CVV no puede estar vacío.")
                .GreaterThan(0).WithMessage("El CVV debe ser mayor que 0.");

            RuleFor(x => x.CreditLimit)
                .GreaterThan(0).WithMessage("El límite de crédito debe ser mayor que 0.");

            RuleFor(x => x.AvaibleCredit)
                .GreaterThanOrEqualTo(0).WithMessage("El crédito disponible no puede ser negativo.");

            RuleFor(x => x.CurrentDebt)
                .GreaterThanOrEqualTo(0).WithMessage("La deuda actual no puede ser negativa.");

            RuleFor(x => x.InterestRate)
                .GreaterThanOrEqualTo(0).WithMessage("La tasa de interés no puede ser negativa.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El ID del cliente no puede estar vacío.")
                .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor que 0.");

            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("El ID de la moneda no puede estar vacío.")
                .GreaterThan(0).WithMessage("El ID de la moneda debe ser mayor que 0.");

            RuleFor(x => x.CreditCardStatus)
                .MaximumLength(50).WithMessage("El estado de la tarjeta de crédito no puede tener más de 50 caracteres.");
        }
    }
}
