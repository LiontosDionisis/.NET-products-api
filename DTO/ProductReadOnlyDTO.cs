namespace ProductsApp.DTO
{
	public class ProductReadOnlyDTO : BaseDTO
	{
		public int Quantity { get; set; }
		public string? ProdName { get; set; }
		public string? ProdDescription { get; set; }
	}
}
