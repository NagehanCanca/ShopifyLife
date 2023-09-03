namespace ShopifyLite.Controllers
{
    internal class ShoppingListItem
    {
        public object ItemName { get; set; }
        public object Quantity { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsBought { get; set; }
    }
}