using FluentValidation;
using ProductsApp.DTO;

namespace ProductsApp.Validators
{
	public class ProductUpdateValidator : AbstractValidator<ProductUpdateDTO>
	{
        public ProductUpdateValidator()
        {
			RuleFor(p => p.Quantity).NotEmpty().WithMessage("Quantity field cannot be empty");
			RuleFor(p => p.ProdName).NotEmpty().WithMessage("Product Name field cannot be empty").Length(1, 50).WithMessage("Name must contain 1-50 characters");
			RuleFor(p => p.ProdDescription).Length(1, 100).WithMessage("Description must contain 1-100 characters");
		}
    }
}
