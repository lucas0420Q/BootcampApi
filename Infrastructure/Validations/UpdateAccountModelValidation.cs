
using Core.Constants;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class UpdateAccountModelValidation : AbstractValidator<UpdateAccountModel>
{
    public UpdateAccountModelValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Account ID cannot be empty")
            .Must(x => x > 0).WithMessage("Invalid Account ID");

        RuleFor(x => x.Holder)
            .MaximumLength(100).WithMessage("Holder cannot be longer than 100 characters");

        RuleFor(x => x.Number)
            .MaximumLength(50).WithMessage("Number cannot be longer than 50 characters");
        //RuleFor(x => x.Type)
        //    .Must(x => Enum.IsDefined(typeof(AccountType), x))
        //    .WithMessage("Invalid Account Type");
        RuleFor(x => x.Balance)
        .GreaterThanOrEqualTo("")
        .WithMessage("Balance must be greater than or equal to zero.");

        RuleFor(x => x.CurrencyId)
            .Must(x => x > 0)
            .WithMessage("Invalid Currency");

        RuleFor(x => x.CustomerId)
            .Must(x => x > 0)
            .WithMessage("Invalid Customer");
    }
}
