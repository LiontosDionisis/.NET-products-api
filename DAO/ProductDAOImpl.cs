using ProductsApp.Models;
using ProductsApp.Services;
using System.Data.SqlClient;

namespace ProductsApp.DAO
{
	public class ProductDAOImpl : IProductDAO
	{
		public void Delete(int id)
		{
			string sql = "DELETE FROM PRODUCTS WHERE ID = @id";
			using SqlConnection conn = DBHelper.GetConnection();
			if (conn is not null) conn.Open();

			using SqlCommand command = new SqlCommand(sql, conn);
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
			conn.Close();		}

        public IList<Product> GetAll()
        {
            string sql = "SELECT * FROM PRODUCTS";
            var products = new List<Product>();

            try
            {
                using SqlConnection conn = DBHelper.GetConnection();
                conn.Open();

                using SqlCommand command = new(sql, conn);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader != null && reader.Read()) 
                {
                    Product product = new Product()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                        ProdName = reader.GetString(reader.GetOrdinal("PROD_NAME")),
                        ProdDescription = reader.GetString(reader.GetOrdinal("PROD_DESC"))
                    };
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error in GetAll: " + ex.Message);
                throw; 
            }

            return products;
        }


        public Product? GetById(int id)
        {
            string sql = "SELECT * FROM PRODUCTS WHERE ID = @id";
            Product? product = null;

            try
            {
                using SqlConnection conn = DBHelper.GetConnection();
                conn.Open();

                using SqlCommand command = new(sql, conn);
                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                        ProdName = reader.GetString(reader.GetOrdinal("PROD_NAME")),
                        ProdDescription = reader.GetString(reader.GetOrdinal("PROD_DESC"))
                    };
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error in GetById: " + ex.Message);
                throw; 
            }

            return product;
        }


        public Product? Insert(Product product)
		{
			if (product == null) return null;
			try
			{
				string sql = "INSERT INTO PRODUCTS (QUANTITY, PROD_NAME, PROD_DESC) VALUES (@quantity, @prod_name, @prod_desc); SELECT SCOPE_IDENTITY();";
				using (SqlConnection conn = DBHelper.GetConnection())
				using (SqlCommand command = new SqlCommand(sql, conn))
				{
					conn.Open();
					command.Parameters.AddWithValue("@quantity", product.Quantity);
					command.Parameters.AddWithValue("@prod_name", product.ProdName);
					command.Parameters.AddWithValue("@prod_desc", product.ProdDescription);
					int insertedId = Convert.ToInt32(command.ExecuteScalar());

					string sqlSelect = "SELECT * FROM PRODUCTS WHERE ID = @id";
					using (SqlCommand selectCommand = new SqlCommand(sqlSelect, conn))
					{
						selectCommand.Parameters.AddWithValue("@id", insertedId);
						using (SqlDataReader reader = selectCommand.ExecuteReader())
						{
							if (reader.Read())
							{
								return new Product
								{
									Id = reader.GetInt32(reader.GetOrdinal("ID")),
									Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY")),
									ProdName = reader.GetString(reader.GetOrdinal("PROD_NAME")),
									ProdDescription = reader.GetString(reader.GetOrdinal("PROD_DESC"))
								};
							}
						}
					}
				}
				return null;
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public Product? Update(Product product)
		{
			if (product == null) return null;
			try
			{
				string sql = "UPDATE PRODUCTS SET QUANTITY = @quantity, PROD_NAME = @prod_name, PROD_DESC = @prod_desc WHERE ID = @id";
				using (SqlConnection conn = DBHelper.GetConnection())
				using (SqlCommand command = new SqlCommand(sql, conn))
				{
					conn.Open();
					command.Parameters.AddWithValue("@quantity", product.Quantity);
					command.Parameters.AddWithValue("@prod_name", product.ProdName);
					command.Parameters.AddWithValue("@prod_desc", product.ProdDescription);
					command.Parameters.AddWithValue("@id", product.Id);
					command.ExecuteNonQuery();
				}
				return product;
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}
