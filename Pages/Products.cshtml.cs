using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsApp.DTO;
using ProductsApp.Models;
using ProductsApp.Services;
using System;
using System.Collections.Generic;

namespace ProductsApp.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public List<ProductReadOnlyDTO> Products { get; set; } = new List<ProductReadOnlyDTO>();
        public Error? ErrorObj { get; set; }

        public ProductsModel(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            try
            {
                var products = _productService!.GetAll(); 
                foreach (var product in products)
                {
                    var productDto = _mapper.Map<ProductReadOnlyDTO>(product);
                    Products.Add(productDto);
                }
            }
            catch (Exception e)
            {
                ErrorObj = new Error("", e.Message, "");
            }

            return Page();
        }
    }
}
