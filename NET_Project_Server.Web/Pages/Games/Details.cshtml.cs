using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NET_Project_Server.Web.Data;
using NET_Project_Server.Web.Models;

namespace NET_Project_Server.Web.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly NET_Project_Server.Web.Data.NET_Project_ServerWebContext _context;

        public DetailsModel(NET_Project_Server.Web.Data.NET_Project_ServerWebContext context)
        {
            _context = context;
        }

        public Game Games { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games.FirstOrDefaultAsync(m => m.Gid == id);
            if (games == null)
            {
                return NotFound();
            }
            else
            {
                Games = games;
            }
            return Page();
        }
    }
}
