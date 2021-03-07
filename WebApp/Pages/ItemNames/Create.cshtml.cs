using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_ItemNames
{
    public class CreateModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public CreateModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ItemName ItemName { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ItemName.Name = string.Join(" ", ItemName.Name.Split(' ').ToList()
                .ConvertAll(word =>
                    word.Substring(0, 1).ToUpper() + word.Substring(1)
                )
            );
            _context.ItemNames.Add(ItemName);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
