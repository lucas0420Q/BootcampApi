
using Core.Entities;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class CreateRequestModelValidation : AbstractValidator<CreateRequestModel>
{
    public CreateRequestModelValidation()
    {

        //RuleFor(x => x.ProductId)
        //    .NotEqual(default(int)).WithMessage("Product ID cannot be default");

        RuleFor(x => x.CurrencyId)
            .NotEqual(default(int)).WithMessage("Currency ID cannot be default");

        RuleFor(x => x.CustomerId)
            .NotEqual(default(int)).WithMessage("Customer ID cannot be default");
    }
}
