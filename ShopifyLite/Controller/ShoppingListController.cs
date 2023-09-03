using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyLite.Data;
using ShopifyLite.Models;
using ShopifyLite.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyLite.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingListController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name;
            var shoppingLists = await _dbContext.ShoppingLists
                .Where(list => list.UserId == userId)
                .ToListAsync();

            return View(shoppingLists);
        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {
            var viewModel = new AddItemViewModel
            {
                ListId = id
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(AddItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shoppingList = await _dbContext.ShoppingLists.FindAsync(model.ListId);

                if (shoppingList != null && shoppingList.UserId == User.Identity.Name)
                {
                    var newItem = new Item
                    {
                        ItemName = model.ItemName,
                        Quantity = model.Quantity
                    };

                    shoppingList.Items.Add(newItem);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }


            return View(model);
        }

        public async Task<IActionResult> SearchItems(string searchTerm, string category)
        {
            var items = await _dbContext.Items.ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                items = items.Where(item => item.ItemName.Contains(searchTerm)).ToList();
            }

            if (!string.IsNullOrEmpty(category))
            {
                items = items.Where(item => item.Category == category).ToList();
            }

            return View(items);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int listId, int itemId)
        {
            var shoppingList = await _dbContext.ShoppingLists.FindAsync(listId);

            if (shoppingList != null && shoppingList.UserId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value)
            {
                var itemToRemove = shoppingList.Items.FirstOrDefault(item => item.Id == itemId);

                if (itemToRemove != null)
                {
                    shoppingList.Items.Remove(itemToRemove);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return RedirectToAction("ItemList", new { id = listId });
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateListName(int id, string newName)
        {
            var shoppingList = await _dbContext.ShoppingLists
                .FirstOrDefaultAsync(list => list.Id == id && list.UserId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            if (shoppingList != null)
            {
                shoppingList.ListName = newName;
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkListAsClosed(int listId)
        {
            var shoppingList = await _dbContext.ShoppingLists
                .FirstOrDefaultAsync(list => list.Id == listId && list.UserId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            if (shoppingList != null)
            {
                shoppingList.IsClosed = true;
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkItemAsBought(int itemId)
        {
            var item = await _dbContext.ShoppingLists.FindAsync(itemId);

            if (item != null)
            {
                item.IsBought = true;
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("ItemList", new { id = item.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CompleteShopping(int id)
        {
            var shoppingList = await _dbContext.ShoppingLists
                .FirstOrDefaultAsync(list => list.Id == id && list.UserId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            if (shoppingList != null)
            {
                shoppingList.IsCompleted = true;
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

    }
}
