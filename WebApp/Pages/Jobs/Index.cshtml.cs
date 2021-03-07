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
    public class IndexModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public IndexModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Job> Job { get;set; } = default!;

        public async Task OnGetAsync(int rallyId)
        {
            RallyId = rallyId;
            Job = await _context.Jobs.Where(job => job.RallyId == RallyId)
                .Include(j => j.JobName)
                .Include(j => j.Rally).ToListAsync();
        }
        [BindProperty]public int RallyId { get; set; }

        public IActionResult OnGetGoToRally(int rallyId)
        {
            return RedirectToPage("../Rally/Index", new {rallyId = rallyId});
        }
        

        
    }
}
