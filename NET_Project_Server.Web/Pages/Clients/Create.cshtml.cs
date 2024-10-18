using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NET_Project_Server.Web.Data;
using NET_Project_Server.Web.Models;

namespace NET_Project_Server.Web.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly NET_Project_Server.Web.Data.NET_Project_ServerWebContext _context;

        public CreateModel(NET_Project_Server.Web.Data.NET_Project_ServerWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Client Client { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Client.Add(Client);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
