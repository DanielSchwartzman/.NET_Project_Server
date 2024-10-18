using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NET_Project_Server.Web.Models;

namespace NET_Project_Server.Web.Data
{
    public class NET_Project_ServerWebContext : DbContext
    {
        public NET_Project_ServerWebContext (DbContextOptions<NET_Project_ServerWebContext> options)
            : base(options)
        {
        }

        public DbSet<NET_Project_Server.Web.Models.Client> Client { get; set; } = default!;
    }
}
