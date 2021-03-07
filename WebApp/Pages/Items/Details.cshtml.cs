using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Items
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public DetailsModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public Item Item { get; set; } = default!;
        
        [BindProperty] public int RallyId { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id,int rallyId)
        {
            RallyId = rallyId;
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Items.Where(item => item.RallyId == RallyId)
                .Include(i => i.Category)
                .Include(i => i.ItemName)
                .Include(i => i.Location)
                .Include(i => i.Rally).FirstOrDefaultAsync(m => m.Id == id);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
