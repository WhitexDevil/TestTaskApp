using FluentValidation;
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.Infrastructure.DataValidators
{
    public class TestEntityValidator : AbstractValidator<TestEntityRequestDto>
    {
        public TestEntityValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The Name cannot be blank.");

            RuleFor(x => x.Priority).SetValidator(new IsInEnumValidator<EntityPriority>());
        }
    }
}