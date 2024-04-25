using ProductsApp.Models;

namespace ProductsApp.DAO
{
	public interface IProductDAO
	{
		Product? Insert(Product product);
		void Delete(int id);
		Product? Update(Product product);
		IList<Product>? GetAll();
		Product? GetById(int id);


	}
}
