using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsApp.Pages
{
    public class DeleteModel : PageModel
    {

        public readonly IProductService? _productService;
        public List<Error> ErrorArray { get; set; } = new();
        public void OnGet(int id)
        {
            try
            {
                 _productService!.Delete(id);
                Response.Redirect("/products/getall")
            } catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
