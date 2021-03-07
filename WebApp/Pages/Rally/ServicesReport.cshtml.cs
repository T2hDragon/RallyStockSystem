using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages.Rally
{
    public class ServicesReport : PageModel
    {
        private readonly ApplicationDbContext _context;


        public ServicesReport( ApplicationDbContext context)
        {
            _context = context;
        }

        public string Location { get; set; } = default!;
        public string Name { get; set; } = default!;
        
        [BindProperty] public int RallyId { get; set; }
        public decimal TotalCost { get; set; } = default!;
        public List<JobPerformed> JobsPerformed = default!;
        public Dictionary<Job, int> JobsCount = default!;
        public Dictionary<Job, decimal> JobCost = default!;
        public Dictionary<Job, Dictionary<Item, int>> JobMaterials = default!;
        public Dictionary<Item, int> AllMaterials = default!;
        
        public async Task OnGetAsync(int rallyId)
        {
            RallyId = rallyId;
            Domain.Rally rally = await _context.Rallies.FirstAsync(e => e.Id == RallyId);
            Name = rally.Name;
            Location = rally.Location;
            JobsPerformed = await _context.JobsPerformed.Where(performed => performed.RallyId == rallyId).
                Include(performed => performed.Job).ThenInclude(job => job.JobName).
                Include(performed => performed.Job).ThenInclude(job => job.JobItems).ThenInclude(jobItem => jobItem.Item).ThenInclude(item => item.ItemName).
                Include(performed => performed.Job).ThenInclude(job => job.JobItems).ThenInclude(jobItem => jobItem.Item).ThenInclude(item => item.Location).
                Include(performed => performed.Job).ThenInclude(job => job.JobItems).ThenInclude(jobItem => jobItem.Item).ThenInclude(item => item.Category).
                ToListAsync();
            JobsCount = new();
            foreach (JobPerformed jobPerformed in JobsPerformed)
            {
                Job? tempJob = JobsCount.Keys.FirstOrDefault(key => key.Id == jobPerformed.JobId);
                if (tempJob == null)
                {
                    JobsCount[jobPerformed.Job] = 1;
                }
                else
                {
                    JobsCount[tempJob] += 1;
                }
            }

            TotalCost = 0;
            SetJobsPerformedPrice(JobsCount);
            AllMaterials = new Dictionary<Item, int>();
            SetJobsPerformedMaterials(JobsCount);

        }

        private void SetJobsPerformedPrice(Dictionary<Job, int> jobsCount)
        {
            JobCost = new Dictionary<Job, decimal>();
            foreach ((Job job, int count) in jobsCount)
            {
                decimal cost = 0;
                foreach (JobItem jobItem in job.JobItems)
                {
                    cost += jobItem.Item.Price * jobItem.ItemQuantity;
                }

                cost *= count;
                TotalCost += cost;
                JobCost[job] = cost;
            }
        }

        private void SetJobsPerformedMaterials(Dictionary<Job, int> jobsCount)
        {
            JobMaterials = new Dictionary<Job, Dictionary<Item, int>>();
            foreach ((Job job,int count) in jobsCount)
            {
                Dictionary<Item, int> material = new ();
                foreach (JobItem jobItem in job.JobItems)
                {
                    if (AllMaterials.ContainsKey(jobItem.Item)) AllMaterials[jobItem.Item] += jobItem.ItemQuantity * count;
                    else AllMaterials[jobItem.Item] = jobItem.ItemQuantity * count;
                    material[jobItem.Item] = jobItem.ItemQuantity * count;
                }

                JobMaterials[job] = material;
            }
        }
        
        public IActionResult OnGetGoToRally(int rallyId)
        {
            return RedirectToPage("./Index", new {rallyId = rallyId});
        }
    }
}