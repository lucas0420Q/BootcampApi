
using Core.Request;
using Core.Requests;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreatePromotionModelValidation : AbstractValidator<CreatePromotionModel>
{
    public CreatePromotionModelValidation()
    {
        RuleFor(x => x.Name)
           .NotNull().WithMessage("Name cannot be null")
           .NotEmpty().WithMessage("Name cannot be empty");

        RuleFor(x => x.Start)
           .NotNull().WithMessage("Start date cannot be null")
           .NotEmpty().WithMessage("Start date cannot be empty")
           .LessThan(x => x.End).WithMessage("Start date must be before end date");

        RuleFor(x => x.End)
           .NotNull().WithMessage("End date cannot be null")
           .NotEmpty().WithMessage("End date cannot be empty")
           .GreaterThan(x => x.Start).WithMessage("End date must be after start date");

        RuleFor(x => x.Discount)
           .NotNull().WithMessage("Discount cannot be null")
           .NotEmpty().WithMessage("Discount cannot be empty")
           .GreaterThan(0).WithMessage("Discount must be greater than zero");
    }
}