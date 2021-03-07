using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Jobs
{
    public class EditModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public EditModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }
        
        [BindProperty]public int RallyId { get; set; }
        [BindProperty]public IList<JobItem> JobItem { get; set; } = default!;


        [BindProperty]
        public Job Job { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int rallyId)
        {
            RallyId = rallyId;
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Jobs.Where(job => job.RallyId == RallyId)
                .Include(j => j.JobName)
                .Include(j => j.Rally).FirstOrDefaultAsync(m => m.Id == id);
            
            
            JobItem = await _context.JobItems.Where(jobItem => jobItem.JobId == id)
                .Include(j => j.Item).ThenInclude(i => i.Category)
                .Include(j => j.Item).ThenInclude(i => i.ItemName)
                .Include(j => j.Item).ThenInclude(i => i.Location)
                .Include(j => j.Job).ToListAsync();

            if (Job == null)
            {
                return NotFound();
            }
            ViewData["JobNameId"] = new SelectList(_context.JobNames, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Job.RallyId = RallyId;

            _context.Attach(Job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(Job.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new{rallyId = RallyId});
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
        
        public async Task<IActionResult> OnGetRemoveItem(int rallyId, int jobItemId, int jobId)
        {
            JobItem jobItem = await _context.JobItems.FirstAsync(item => item.Id == jobItemId);
            _context.JobItems.Remove(jobItem);
            await _context.SaveChangesAsync();
            return RedirectToPage("", new {id = jobId, rallyId = rallyId});
        }
    }
}
