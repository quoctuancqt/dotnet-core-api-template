using FluentValidation;

namespace Demo.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
