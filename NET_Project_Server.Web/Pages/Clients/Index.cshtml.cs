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
    public class IndexModel : PageModel
    {
        private readonly NET_Project_Server.Web.Data.NET_Project_ServerWebContext _context;

        public IndexModel(NET_Project_Server.Web.Data.NET_Project_ServerWebContext context)
        {
            _context = context;
        }

        public IList<Models.Client> Client { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Client = await _context.Client.ToListAsync();
        }
    }
}
