using System.ComponentModel.DataAnnotations;

namespace ShopifyLite.Models { 

public class Item
{
    public int Id { get; set; }

    [Required]
    public string ItemName { get; set; }

    [Required]
    public int Quantity { get; set; }

    public string Category { get; set; } // Öğelerin kategori bilgisi (isteğe bağlı)

    // Diğer özellikler veya ilişkiler buraya eklenebilir
}
}
