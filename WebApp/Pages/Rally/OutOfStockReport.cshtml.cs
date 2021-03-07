using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Rally
{
    public class OutOfStockReport : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public OutOfStockReport(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public int RallyId { get; set; }
        public List<Item> Items { get; set; } = default!;

        public async Task OnGetAsync(int rallyId)
        {
            RallyId = rallyId;
            Items = await _context.Items.Where(item => item.CurrentQuantity < item.OptimalQuantity && item.RallyId == RallyId)
                .Include(item => item.Category)
                .Include(item => item.Location)
                .Include(item => item.ItemName)
                .ToListAsync();
        }
        
        
        public IActionResult OnGetGoToRally(int rallyId)
        {
            return RedirectToPage("./Index", new {rallyId = rallyId});
        }
    }
}