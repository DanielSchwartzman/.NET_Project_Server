using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NET_Project_Server.Web.Data;
using NET_Project_Server.Web.Models;

namespace NET_Project_Server.Web.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly NET_Project_Server.Web.Data.NET_Project_ServerWebContext _context;

        public DetailsModel(NET_Project_Server.Web.Data.NET_Project_ServerWebContext context)
        {
            _context = context;
        }

        public Models.Client Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }
            else
            {
                Client = client;
            }
            return Page();
        }
    }
}
