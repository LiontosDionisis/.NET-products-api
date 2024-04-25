using ProductsApp.DTO;
using ProductsApp.Models;

namespace ProductsApp.Services
{
	public interface IProductService
	{
		void Delete(int id);
		IList<Product> GetAll();
		Product GetById(int id);
		Product Insert(ProductInsertDTO dto);
		Product Update(ProductUpdateDTO dto);
	}
}
