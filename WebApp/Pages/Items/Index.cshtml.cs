using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebApp.Pages_Items
{
    public class IndexModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public IndexModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;
        [BindProperty]public int MinQuantity { get;set; }
        [BindProperty]public int MaxQuantity { get;set; }
        [BindProperty]public bool Filter { get;set; }

        public async Task OnGetAsync(int rallyId, string? inclusiveItemPartsJson, string? exclusiveItemPartsJson, 
            int minQuantity, int maxQuantity, int categoryId, bool? filter)
        {
            Filter = filter ?? false;
            List<Category> categories = new List<Category>(){new Category(){Id = 0, Name = ""}};
            categories.AddRange(await _context.Categories.ToListAsync());
            CategoryId = categoryId;
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", categories.First(category => category.Id == CategoryId));
            MinQuantity = minQuantity;
            MaxQuantity = maxQuantity;
            RallyId = rallyId;
            InclusiveItemNameParts = inclusiveItemPartsJson == null ? new List<string>():JsonConvert.DeserializeObject<List<string>>(inclusiveItemPartsJson);
            ExclusiveItemNameParts = exclusiveItemPartsJson == null ? new List<string>():JsonConvert.DeserializeObject<List<string>>(exclusiveItemPartsJson);

            if (filter.HasValue && filter == true)
            {
                Item = new List<Item>();
                List<Item> inclusiveItems = new List<Item>();
                List<Item> exclusiveItems = new List<Item>();
                List<Item> baseItems = await _context.Items.Where(item => item.CurrentQuantity >= MinQuantity
                                                            && (item.CurrentQuantity <= MaxQuantity || MaxQuantity == 0)
                                                            && (item.Category.Id == CategoryId || CategoryId == 0)
                                                            && item.Rally.Id == RallyId).
                    Include(item => item.Category).
                    Include(item => item.ItemName).
                    Include(item => item.Location).ToListAsync();
                foreach (string itemNamePart in InclusiveItemNameParts)
                {
                    inclusiveItems.AddRange(
                        baseItems.Where(item => item.ItemName.Name.ToLower().Contains(itemNamePart.ToLower()))
                    );
                }

                if (InclusiveItemNameParts.Count == 0) inclusiveItems = baseItems;
                foreach (string itemNamePart in ExclusiveItemNameParts)
                {
                    exclusiveItems.AddRange(
                        baseItems.Where(item => item.ItemName.Name.ToLower().Contains(itemNamePart.ToLower()))
                    );
                }

                Item = inclusiveItems.Where(item => !exclusiveItems.Contains(item)).ToList();

            } else Item = await _context.Items.Where(item => item.RallyId == rallyId)
                .Include(i => i.Category)
                .Include(i => i.ItemName)
                .Include(i => i.Location)
                .Include(i => i.Rally).ToListAsync();

        }

        public int RallyId { get; set; }
        [BindProperty]public List<string> InclusiveItemNameParts { get; set; } = default!;
        [BindProperty]public List<string> ExclusiveItemNameParts { get; set; } = default!;
 
        [BindProperty]public int CategoryId { get; set; }
        
        
        
        [Range(1, 10000)]
        [BindProperty]
        public int ItemAddAmount { get; set; }

        public IActionResult OnGetGoToRally(int rallyId)
        {
            return RedirectToPage("../Rally/Index", new {rallyId = rallyId});
        }
        
        public IActionResult OnPostFilter(int rallyId)
        {
            string inclusiveItemPartsJson = JsonConvert.SerializeObject(InclusiveItemNameParts.Where(s => s.Trim() != ""));
            string exclusiveItemPartsJson = JsonConvert.SerializeObject(ExclusiveItemNameParts.Where(s => s.Trim() != ""));
            return RedirectToPage("", new {rallyId = rallyId, 
                inclusiveItemPartsJson = inclusiveItemPartsJson, exclusiveItemPartsJson = exclusiveItemPartsJson, 
                minQuantity = MinQuantity, maxQuantity = MaxQuantity, 
                categoryId = CategoryId,
                filter = true
            }
            );
        }
        
            
        public IActionResult OnPostModifyNamePart(int rallyId, int index, bool exclusive, bool add, int oppositeMax, bool? filter, int categoryId)
        {
            if (exclusive)
            {
                if (add)ExclusiveItemNameParts.Add("");
                else
                {
                    if (oppositeMax == 0) InclusiveItemNameParts = new List<string>();
                    ExclusiveItemNameParts.RemoveAt(index);
                }
            }
            else
            {
                if (add)InclusiveItemNameParts.Add("");
                else
                {
                    if (oppositeMax == 0) ExclusiveItemNameParts = new List<string>();
                    InclusiveItemNameParts.RemoveAt(index);
                }
            }
            
            string inclusiveItemPartsJson = JsonConvert.SerializeObject(InclusiveItemNameParts);
            string exclusiveItemPartsJson = JsonConvert.SerializeObject(ExclusiveItemNameParts);
            return RedirectToPage("", new {rallyId = rallyId, inclusiveItemPartsJson = inclusiveItemPartsJson, exclusiveItemPartsJson = exclusiveItemPartsJson, minQuantity = MinQuantity, maxQuantity = MaxQuantity, filter= filter ?? false, categoryId = categoryId});
        }

        public async Task<IActionResult> OnPostAddToStock(int rallyId, int itemId)
        {
            Item item = await _context.Items.FirstAsync(item => item.Id == itemId);
            item.CurrentQuantity += ItemAddAmount;
            await _context.SaveChangesAsync();
            return RedirectToPage("", new {rallyId});
        }
        
    }
}
