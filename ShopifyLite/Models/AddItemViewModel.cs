using System.ComponentModel.DataAnnotations;

namespace ShopifyLite.ViewModels
{
public class AddItemViewModel
{
    public int ListId { get; set; }

    [Required]
    public string ItemName { get; set; }

    [Required]
    public int Quantity { get; set; }

    public string ListName { get; set; }

    public int Id { get; internal set; }
}
}
