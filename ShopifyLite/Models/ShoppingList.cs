using Fluent.Infrastructure.FluentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ShoppingList
{
    public int Id { get; set; }

    [Required]
    public string ListName { get; set; }

    public DateTime CreatedAt { get; set; }

    // Relationships
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    // Define a navigation property for the list of items
    public List<AddItemViewModel> Items { get; set; } // Use List<Item> to represent the collection of items

    public bool IsCompleted { get; set; }
    public bool IsClosed { get; set; }
    public bool IsBought { get; set; }
}
