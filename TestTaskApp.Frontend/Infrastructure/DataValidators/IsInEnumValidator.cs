using System;
using FluentValidation.Validators;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.Infrastructure.DataValidators
{
    public class IsInEnumValidator<T> : PropertyValidator
    {

        public IsInEnumValidator()
            : base("Property {PropertyName} it not a valid enum value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (!typeof(T).IsEnum) return false;

            return Enum.IsDefined(typeof(EntityPriority), context.PropertyValue);
        }
    }
}