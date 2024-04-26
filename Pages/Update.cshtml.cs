using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsApp.DTO;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsApp.Pages
{
    public class UpdateModel : PageModel
    {
        public List<Error>? ErrorArray { get; set; } = new();
        public ProductUpdateDTO ProductUpdateDTO { get; set; } = new();

        public readonly IProductService _productService;
        public readonly IMapper _mapper;
        public readonly IValidator<ProductUpdateDTO> _validator;

		public UpdateModel(IProductService productService, IMapper mapper, IValidator<ProductUpdateDTO> validator)
		{
			_productService = productService;
			_mapper = mapper;
			_validator = validator;
		}

		public IActionResult OnGet(int id)
        {
			try
			{
				Product? product = _productService!.GetById(id);
				ProductUpdateDTO = _mapper!.Map<ProductUpdateDTO>(product);
			}
			catch (Exception e)
			{
				ErrorArray!.Add(new Error("", e.Message, ""));
			}
			return Page();

		}

		public void OnPost(ProductUpdateDTO dto)
		{
			ProductUpdateDTO = dto;

			var validationResult = _validator!.Validate(dto);

			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					ErrorArray!.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
				}
				return;
			}

			try
			{
				Product? product = _productService!.Update(dto);
				Response.Redirect("/products/getall");
			}
			catch (Exception e)
			{
				ErrorArray!.Add(new Error("", e.Message, ""));
			}
		}
    }
}
