using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Rallies
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public DeleteModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rally Rally { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rally = await _context.Rallies.FirstOrDefaultAsync(m => m.Id == id);

            if (Rally == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rally = await _context.Rallies.FindAsync(id);

            if (Rally != null)
            {
                List<JobPerformed> jobsPerformed = await _context.JobsPerformed
                    .Where(performed => performed.RallyId == Rally.Id).ToListAsync();
                _context.JobsPerformed.RemoveRange(jobsPerformed);
                _context.Rallies.Remove(Rally);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
