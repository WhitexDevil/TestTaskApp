using FluentValidation.Attributes;
using TestTaskApp.Frontend.Infrastructure.DataValidators;

namespace TestTaskApp.Frontend.DTOs.Request
{
    [Validator(typeof(TestEntityValidator))]
    public class TestEntityRequestDto : BaseTestEntityDto
    {

    }
}
