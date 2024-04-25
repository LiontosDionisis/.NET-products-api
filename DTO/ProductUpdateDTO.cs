namespace ProductsApp.DTO
{
	public class ProductUpdateDTO : BaseDTO
	{
		public int Quantity { get; set; }
		public string? ProdName { get; set; }
		public string? ProdDescription { get; set; }
	}
}
