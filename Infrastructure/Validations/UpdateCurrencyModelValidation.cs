
using Core.Constants;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class UpdateCurrencyModelValidation : AbstractValidator<UpdateCurrencyModel>
{
    public UpdateCurrencyModelValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty");

        RuleFor(x => x.BuyValue)
            .NotNull().WithMessage("BankId cannot be null")
            .NotEmpty().WithMessage("BankId cannot be empty");
        RuleFor(x => x.SellValue)
            .NotNull().WithMessage("DocumentNumber cannot be null")
            .NotEmpty().WithMessage("DocumentNumber cannot be empty");
    }
}
