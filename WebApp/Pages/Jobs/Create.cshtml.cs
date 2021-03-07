using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_Jobs
{
    public class CreateModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public CreateModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int rallyId)
        {
            RallyId = rallyId;
        ViewData["JobNameId"] = new SelectList(_context.JobNames, "Id", "Name");
            return Page();
        }

        
        [BindProperty]public int RallyId { get; set; }

        
        [BindProperty]
        public Job Job { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Job.RallyId = RallyId;

            await _context.Jobs.AddAsync(Job);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new {rallyId = RallyId});
        }
    }
}
