using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApp.Pages.Rally
{
    public class Index : PageModel
    {
        private readonly ApplicationDbContext _context;


        public Index(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;

        public List<string>? ErrorMessages { get; set; }
        
        [BindProperty]public Domain.Rally Rally { get; set; } = default!;

        
        public Job[] Jobs { get; set; } = default!;
        [BindProperty] public int PerformJobId { get; set; }

        public async Task OnGetAsync(int rallyId, string? errorsJson)
        {
            if (errorsJson != null)
            {
                ErrorMessages = JsonConvert.DeserializeObject<List<string>>(errorsJson);
            }
            Rally = await _context.Rallies.Include(rally => rally.Jobs).ThenInclude(job => job.JobName).FirstAsync(e => e.Id == rallyId);
            Name = Rally.Name;
            Location = Rally.Location;
            Jobs = (Rally.Jobs ?? throw new InvalidOperationException("Rally has no Jobs")).ToArray();
        }
        
        public IActionResult OnGetLoadRallyItems(int rallyId)
        {
            return RedirectToPage("../Items/Index", new {rallyId = rallyId, inclusiveItemPartsJson = "[\"\"]"});
        }
        
        public IActionResult OnGetLoadRallyJobs(int rallyId)
        {
            return RedirectToPage("../Jobs/Index", new {rallyId = rallyId});
        }

        public async Task<IActionResult> OnPostPerformJob(int rallyId)
        {
            Job job = await _context.Jobs.Include(e => e.JobItems).ThenInclude(item => item.Item).ThenInclude(item => item.ItemName).
                Include(e => e.JobItems).ThenInclude(item => item.Item).ThenInclude(item => item.Category).
                Include(e => e.JobItems).ThenInclude(item => item.Item).ThenInclude(item => item.Location).
                FirstAsync(e => e.Id == PerformJobId);
            List<string> errorMessages = new();
            foreach (JobItem jobItem in job.JobItems)
            {
                if (jobItem.ItemQuantity > jobItem.Item.CurrentQuantity)
                {
                    errorMessages.Add(
                        $"{jobItem.Item.Category.Name} in {jobItem.Item.Location.Name} is missing {jobItem.ItemQuantity - jobItem.Item.CurrentQuantity} {jobItem.Item.ItemName.Name}(s)!");
                }
            }

            if (errorMessages.Count == 0)
            {
                foreach (JobItem jobItem in job.JobItems)
                {
                    jobItem.Item.CurrentQuantity -= jobItem.ItemQuantity;
    
                }
                await _context.JobsPerformed.AddAsync(new JobPerformed()
                {
                    Job = job,
                    RallyId = rallyId
                });
                await _context.SaveChangesAsync();
            }
            string json = JsonConvert.SerializeObject(errorMessages);
            return RedirectToPage("", new {rallyId = rallyId, errorsJson = json});

        }
        
        public IActionResult OnGetServicesReport(int rallyId)
        {
            return RedirectToPage("./ServicesReport", new {rallyId = rallyId});
        }
        
        public IActionResult OnGetOutOfStockReport(int rallyId)
        {
            return RedirectToPage("./OutOfStockReport", new {rallyId = rallyId});
        }
    }
}