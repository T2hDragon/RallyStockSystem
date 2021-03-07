using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages_Jobs_Items
{
    public class CreateModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public CreateModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int rallyId, int jobId)
        {
            RallyId = rallyId;
            JobId = jobId;
            List<Item> items = await _context.Items.Where(item => item.RallyId == RallyId).Include(item => item.ItemName)
                .Include(item => item.Category).Include(item => item.Location).ToListAsync();
            List<object> itemsWithDescription = new();
            foreach (Item item in items)
            {
                itemsWithDescription.Add( new
                {
                    Id = item.Id,
                    Description =
                        $"Category: {item.Category.Name} " +
                        $"Name: {item.ItemName.Name} in " +
                        $"Location: {item.Location.Name}" 
                });
            }
            ViewData["ItemId"] = new SelectList(itemsWithDescription,
                "Id", "Description");

            return Page();
        }

        [BindProperty]public int ItemId { get; set; }
        
        [BindProperty]public int RallyId { get; set; }
        [BindProperty]public int JobId { get; set; }
        
        [BindProperty]public int Quantity { get; set; }

        

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.JobItems.Add(new JobItem()
            {
                ItemId = ItemId,
                JobId = JobId,
                ItemQuantity = Quantity
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("../Edit", new{rallyId = RallyId, id = JobId});
        }

    }
}
