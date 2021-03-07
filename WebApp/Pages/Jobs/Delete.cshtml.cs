using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Jobs
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public DeleteModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }
        
        [BindProperty] public int RallyId { get; set; }

        
        [BindProperty]
        public Job Job { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id,int rallyId)
        {
            RallyId = rallyId;
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Jobs.Where(job => job.RallyId == RallyId)
                .Include(j => j.JobName)
                .Include(j => j.Rally).FirstOrDefaultAsync(m => m.Id == id);

            if (Job == null)
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

            Job = await _context.Jobs.FindAsync(id);

            if (Job != null)
            {
                List<JobItem> jobItems = await _context.JobItems.Where(item => item.JobId == Job.Id).ToListAsync();
                _context.JobItems.RemoveRange(jobItems);
                _context.Jobs.Remove(Job);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new{rallyId = RallyId});
        }
    }
}
