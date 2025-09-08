namespace Proyecto_Nexus.ViewModels
{
    public class CartItemViewModel
    {
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalItemPrice { get; set; }
    }
}