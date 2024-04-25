using AutoMapper;
using ProductsApp.DAO;
using ProductsApp.DTO;
using ProductsApp.Models;
using System.Transactions;

namespace ProductsApp.Services
{
	public class ProductServiceImpl : IProductService
	{
		private readonly IProductDAO _productDAO;
		private readonly IMapper _mapper;
		private readonly ILogger<ProductServiceImpl> _logger;

		public ProductServiceImpl(IProductDAO productDAO, IMapper mapper, ILogger<ProductServiceImpl> logger)
		{
			_productDAO = productDAO;
			_mapper = mapper;
			_logger = logger;
		}

		public void Delete(int id)
		{
			Product? productToDelete = null;
			try
			{
				using TransactionScope scope = new();
				productToDelete = _productDAO.GetById(id);
				if (productToDelete != null)
				{
					_productDAO.Delete(productToDelete.Id);
				}
				scope.Complete();
				_logger.LogInformation("Product Deleted");
			}
			catch (Exception e)
			{
				_logger.LogError("Error in deleting product", e.Message);
				throw;
			}
		}

        public IList<Product> GetAll()
        {
            try
            {
                IList<Product> products = _productDAO.GetAll() ?? new List<Product>(); 
                return products;
            }
            catch (Exception e)
            {
                _logger.LogError("Error in fetching all products", e.Message);
                throw;
            }
        }

        public Product GetById(int id)
		{
			try
			{
				return _productDAO.GetById(id);
			}
			catch (Exception e)
			{
				_logger.LogError("Error fetching product", e.Message);
				throw;
			}
		}

		public Product Insert(ProductInsertDTO dto)
		{
			if (dto == null) throw new ArgumentNullException(nameof(dto));
			try
			{
				var product = _mapper.Map<Product>(dto);
				using (TransactionScope scope = new TransactionScope())
				{
					Product? insertedProduct = _productDAO.Insert(product);
					scope.Complete();
					_logger.LogInformation("Product inserted");
					return insertedProduct!;
				}
			}
			catch (Exception e)
			{
				_logger.LogError("Error inserting product", e.Message);
				throw;
			}
		}

		public Product Update(ProductUpdateDTO dto)
		{
			if (dto == null) throw new ArgumentNullException(nameof(dto));
			try
			{
				var product = _mapper.Map<Product>(dto);
				using (TransactionScope scope = new TransactionScope())
				{
					Product? updatedProduct = _productDAO.Update(product);
					scope.Complete();
					_logger.LogInformation("Product updated!");
					return updatedProduct!;
				}
			}
			catch (Exception e)
			{
				_logger.LogError("Error updating product", e.Message);
				throw;
			}
		}
	}
}
