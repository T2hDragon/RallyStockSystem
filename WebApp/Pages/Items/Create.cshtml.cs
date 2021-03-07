using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_Items
{
    public class CreateModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public CreateModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int rallyId, int jobId)
        {
            RallyId = rallyId;
            JobId = jobId;
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["ItemNameId"] = new SelectList(_context.ItemNames, "Id", "Name");
        ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            return Page();
        }

        [BindProperty] public Item Item { get; set; } = default!;
        
        [BindProperty]public int RallyId { get; set; }

        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [BindProperty]
        public string Price { get; set; } = default!;

        [BindProperty]public int JobId { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Item.RallyId = RallyId;
            Item.Price = decimal.Parse(Price.Replace(".", ","));
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new{rallyId = RallyId, id = JobId});
        }

    }
}
