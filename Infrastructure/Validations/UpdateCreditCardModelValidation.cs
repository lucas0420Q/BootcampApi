using Core.Constants;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;


public class UpdateCreditCarsModelValidation : AbstractValidator<UpdateCreditCardModel>
{
    public UpdateCreditCarsModelValidation()
    {
        RuleFor(x => x.Designation)
            .NotNull().WithMessage("Designation cannot be null")
            .NotEmpty().WithMessage("Designation cannot be empty");

        RuleFor(x => x.IssueDate)
            .NotNull().WithMessage("IssueDate cannot be null")
            .NotEmpty().WithMessage("IssueDate cannot be empty");
        RuleFor(x => x.ExpirationDate)
            .NotNull().WithMessage("ExpirationDate cannot be null")
            .NotEmpty().WithMessage("ExpirationDate cannot be empty");

        RuleFor(x => x.CardNumber)
            .NotNull().WithMessage("CardNumber cannot be null")
            .NotEmpty().WithMessage("CardNumber cannot be empty");
        RuleFor(x => x.Cvv)
            .NotNull().WithMessage("Cvv cannot be null")
            .NotEmpty().WithMessage("Cvv cannot be empty");
        RuleFor(x => x.CreditCardStatus)
            .Must(x => Enum.IsDefined(typeof(CreditCardStatus), x))
            .WithMessage("Invalid CreditCard Status");
        RuleFor(x => x.CreditLimit)
            .NotNull().WithMessage("CreditLimit cannot be null")
            .NotEmpty().WithMessage("CreditLimit cannot be empty");
        RuleFor(x => x.AvailableCredit)
            .NotNull().WithMessage("AvailableCredit cannot be null")
            .NotEmpty().WithMessage("AvailableCredit cannot be empty");
        RuleFor(x => x.CurrentDebt)
            .NotNull().WithMessage("CurrentDebt cannot be null")
            .NotEmpty().WithMessage("CurrentDebt cannot be empty");
        RuleFor(x => x.InterestRate)
            .NotNull().WithMessage("InterestRate cannot be null")
            .NotEmpty().WithMessage("InterestRate cannot be empty");

    }
}
