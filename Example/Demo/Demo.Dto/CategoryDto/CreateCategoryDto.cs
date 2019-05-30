using FluentValidation;

namespace Demo.Dto
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
    }

    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
