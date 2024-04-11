using Core.Constants;
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
                .NotEmpty().WithMessage("Cvv cannot be empty")
                .Must(w => w.ToString().Length >= 3 && w.ToString().Length <= 4)
                .WithMessage("CVV must be between 3 and 4 digits"); ;
            RuleFor(x => x.CreditCardStatus)
                .Must(x => Enum.IsDefined(typeof(CreditCardStatus), x))
                .WithMessage("Invalid CreditCard Status");
            RuleFor(x => x.CreditLimit)
                .NotNull().WithMessage("CreditLimit cannot be null")
                .NotEmpty().WithMessage("CreditLimit cannot be empty");
            RuleFor(x => x.CreditLimit)
                .NotNull().WithMessage("Credit Limit cannot be null")
                .GreaterThan(0).WithMessage("Credit Limit must be greater than zero.");
            RuleFor(x => x.AvailableCredit)
                .NotNull().WithMessage("Available Credit cannot be null")
                .GreaterThan(500000).WithMessage("Interest must be greater than five hundred thousand.");

            RuleFor(x => x.InterestRate)
                .NotNull().WithMessage("Interest Rate cannot be null")
                .GreaterThan(0).WithMessage("Interest must be greater than zero.");
        }
    }
}