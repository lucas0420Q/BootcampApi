
using Core.Constants;
using Core.Request;
using Core.Requests;
using FluentValidation;

namespace Infrastructure.Validetions;

public class CreateAccountModelValidation : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountModelValidation()
    {
        RuleFor(x => x.Holder)
            .NotNull().WithMessage("Holder cannot be null")
            .NotEmpty().WithMessage("Holder cannot be empty")
            .MaximumLength(100).WithMessage("Holder cannot be longer than 100 characters");
        RuleFor(x => x.Number)
            .NotNull().WithMessage("Number cannot be null")
            .NotEmpty().WithMessage("Number cannot be empty")
            .MaximumLength(50).WithMessage("Number cannot be longer than 50 characters");
        RuleFor(x => x.AccountType)
            .Must(x => Enum.IsDefined(typeof(AccountType), x))
            .WithMessage("Invalid Account Type");
        //RuleFor(x => x.Balance)
        //    .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to zero.");
        RuleFor(x => x.CurrencyId)
            .NotEmpty().WithMessage("Currency cannot be empty")
            .Must(x => x > 0).WithMessage("Invalid Currency");
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer cannot be empty")
            .Must(x => x > 0).WithMessage("Invalid Customer");
    }
}
