using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace ServiceBrain
{
    public class Rally
    {
        private readonly ApplicationDbContext _dbContext;
        public readonly int RallyId;
        private readonly Domain.Rally _rally;


        public Rally(ApplicationDbContext dbContext, int rallyId)
        {
            _dbContext = dbContext;
            RallyId = rallyId;
            _rally = _dbContext.Rallies.Include(rally => rally.Jobs).ThenInclude(job => job.JobName).FirstAsync(rally => rally.Id == rallyId).Result;
        }

        public string GetName()
        {
            return _rally.Name;
        }
        
        public string GetLocation()
        {
            return _rally.Location;
        }
        
        public async Task AddItem(int itemNameId, int currentQuantity, int optimalQuantity, decimal price, int categoryId, int locationId)
        {
            ItemName itemName = await _dbContext.ItemNames.FirstAsync(name => name.Id == itemNameId);
            Category category = await _dbContext.Categories.FirstAsync(name => name.Id == categoryId);
            Location location = await _dbContext.Locations.FirstAsync(name => name.Id == locationId);
            await _dbContext.Items.AddAsync(new Item()
            {
                CurrentQuantity = currentQuantity,
                OptimalQuantity = optimalQuantity,
                Price = price,
                ItemName = itemName,
                Category = category,
                Location = location,
                Rally = _rally
            });
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task AddJob(int jobNameId)
        {
            JobName jobName = await _dbContext.JobNames.FirstAsync(name => name.Id == jobNameId);
            await _dbContext.Jobs.AddAsync(new Job()
            {
                JobName = jobName,
                Rally = _rally,
                JobItems = new List<JobItem>()
            });
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task AddItemJob(int itemId, int jobId)
        {

            Job job = await _dbContext.Jobs.FirstAsync(e => e.Id == jobId);
            Item item = await _dbContext.Items.FirstAsync(e => e.Id == itemId);
            job.JobItems.Add(new JobItem()
            {
                Job = job,
                Item = item
            });
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<Item[]> GetItemsNeeded()  // <Item, missing quantity>
        {
            return await _dbContext.Items.Where(item => item.CurrentQuantity < item.OptimalQuantity)
                .Include(item => item.ItemName)
                .Include(item => item.Category)
                .Include(item => item.Location).ToArrayAsync();
        }

        public async Task<Item[]> GetItems(string itemNamePart, int minPrice, int maxPrice, int minQuantity,  int maxQuantity, string categoryNamePart, string locationNamePart)
        {
            return await _dbContext.Items.Where(item => item.ItemName.Name.Contains(itemNamePart)
                                                               && item.CurrentQuantity >= minQuantity
                                                               && item.CurrentQuantity <= maxQuantity
                                                               && item.Price >= minPrice
                                                               && item.Price <= maxPrice
                                                               && item.Category.Name.Contains(categoryNamePart)
                                                               && item.Location.Name.Contains(locationNamePart)
                                                               && item.Rally.Id == RallyId).
                Include(item => item.Category).
                Include(item => item.ItemName).
                Include(item => item.Location).ToArrayAsync();
        }
        
        public async Task<Job[]> GetJobs(string jobNamePart)
        {
            return await _dbContext.Jobs.Where(job => job.JobName.Name.Contains(jobNamePart))
                .Include(job => job.JobName).ToArrayAsync();
        }
        
        public async Task<List<Item>> GetJobItems(int jobId)
        {
            List<Item> items = new();
            JobItem[] jobItems = await _dbContext.JobItems.Where(item => item.JobId == jobId).ToArrayAsync();
            foreach (JobItem jobItem in jobItems)
            {
                Item item = await _dbContext.Items.FirstAsync(item =>
                    item.JobItems != null && item.JobItems.Contains(jobItem));
                if (items.Contains(item)) continue;
                items.Add(item);
            }

            return items;
        }
        
        public bool PerformJob(int jobId)
        {
            //TODO 
            throw new Exception("Not Implemented");
        }
        public Dictionary<string, string> GetReport()
        {
            //TODO 
            throw new Exception("Not Implemented");
        }
        
    }
}