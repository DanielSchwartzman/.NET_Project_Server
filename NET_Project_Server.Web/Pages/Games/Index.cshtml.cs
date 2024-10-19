using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NET_Project_Server.Web.Data;
using NET_Project_Server.Web.Models;

namespace NET_Project_Server.Web.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly NET_Project_Server.Web.Data.NET_Project_ServerWebContext _context;
        public List<string> TableOfContentsItems { get; set; } = new List<string>();

        public IndexModel(NET_Project_Server.Web.Data.NET_Project_ServerWebContext context)
        {
            _context = context;
        }

        // Properties to hold query results
        public IList<Client> AllPlayers { get; set; }
        public IList<PlayerWithGameDate> PlayersWithLastGame { get; set; }
        public IList<Game> AllGames { get; set; }

        public IList<String?> AllPlayerNames { get; set; }

        [BindProperty]
        public string SelectedPlayerName { get; set; }
        public IList<Client> FirstPlayersByCountry { get; set; }
        public IList<PlayerWithGameCount> PlayersWithGameCount { get; set; }
        public IList<PlayerGroupedByGames> PlayersGroupedByGames { get; set; }
        public IList<PlayerGroupedByCountry> PlayersGroupedByCountry { get; set; }
        public string SelectedQuery { get; set; }

        public class PlayerWithGameDate
        {
            public string Name { get; set; }
            public DateTime LastGameDate { get; set; }
        }

        public class PlayerWithGameCount
        {
            public string Name { get; set; }
            public int GameCount { get; set; }
        }

        public class PlayerGroupedByGames
        {
            public int GameCount { get; set; }
            public IList<Client> Players { get; set; }
        }

        public class PlayerGroupedByCountry
        {
            public string Country { get; set; } // Country name
            public IList<Client> Players { get; set; } // List of players from that country
        }

        public async Task<IActionResult> OnGetAsync()
        {
            SelectedQuery = "playersWhoPlayed";
            await LoadData();
            return Page();
        }

        public async Task<IActionResult> OnPostForm1Async(string query)
        {
            SelectedQuery = query;
            await LoadData();
            return Page();
        }

        public async Task<IActionResult> OnPostForm2Async()
        {
            SelectedQuery = "Query22";
            AllPlayers = await _context.Client
                   .FromSqlRaw("SELECT DISTINCT c.Name, c.ID, c.Phone, c.Country, LOWER(c.Name) AS LoweredName FROM dbo.Client c JOIN dbo.Games g ON c.ID = g.Pid ORDER BY LoweredName;")
                   .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostForm3Async()
        {
            AllPlayerNames = await _context.Client
                    .Select(c => c.Name)
                    .Distinct()
                    .OrderBy(n => n.ToLower())
                    .ToListAsync();

            string clientName = SelectedPlayerName;
            SelectedQuery = "Query26";
            PlayersWithGameCount = await _context.Client
    .Where(c => c.Name.ToLower() == clientName.ToLower()) // Case-insensitive comparison
    .Select(c => new PlayerWithGameCount
    {
        Name = c.Name,
        GameCount = _context.Games.Count(g => g.Pid == c.ID)
    })
    .ToListAsync();
            return Page();
        }

        private async Task LoadData()
        {
            if (SelectedQuery == "Query22")
            {
                AllPlayers = await _context.Client
                    .FromSqlRaw("SELECT DISTINCT c.Name, c.ID, c.Phone, c.Country FROM Client c JOIN Games g ON c.ID = g.Pid ORDER BY c.ID")
                    .ToListAsync();
            }

            else if (SelectedQuery == "Query23")
            {
                PlayersWithLastGame = await _context.Client
                    .Join(_context.Games, c => c.ID, g => g.Pid, (c, g) => new { c.Name, g.Date })
                    .GroupBy(x => x.Name)
                    .Select(g => new PlayerWithGameDate
                    {
                        Name = g.Key,
                        LastGameDate = g.Max(x => x.Date)
                    })
                    .OrderByDescending(p => p.Name)
                    .ToListAsync();
            }

            else if (SelectedQuery == "Query24")
            {
                AllGames = await _context.Games.ToListAsync();
            }

            else if (SelectedQuery == "Query25")
            {
                FirstPlayersByCountry = await _context.Client
                    .FromSqlRaw("SELECT ID, Name, Phone, Country FROM (SELECT c.ID, c.Name, c.Phone, c.Country, ROW_NUMBER() OVER (PARTITION BY c.Country ORDER BY MIN(g.Date)) AS rn FROM Client c JOIN Games g ON c.ID = g.Pid GROUP BY c.ID, c.Name, c.Phone, c.Country) AS FirstPlayers WHERE rn = 1 ORDER BY Country;")
                    .ToListAsync();
            }

            else if (SelectedQuery == "Query26")
            {
                AllPlayerNames = await _context.Client
                    .Select(c => c.Name)
                    .Distinct()
                    .OrderBy(n => n.ToLower())
                    .ToListAsync();
            }

            else if (SelectedQuery == "Query27")
            {
                PlayersGroupedByGames = await _context.Client
                    .GroupBy(c => c.ID)
                    .Select(g => new PlayerGroupedByGames
                    {
                        GameCount = g.Count(),
                        Players = g.ToList()
                    })
                    .ToListAsync();
            }

            else if (SelectedQuery == "Query28")
            {
                PlayersGroupedByCountry = await _context.Client
                    .GroupBy(c => c.Country)
                    .Select(g => new PlayerGroupedByCountry
                    {
                        Country = g.Key,
                        Players = g.ToList()
                    })
                    .ToListAsync();
            }

            else if (SelectedQuery == "Query29")
            {
                PlayersGroupedByCountry = await _context.Client
                    .GroupBy(c => c.Country)
                    .Select(g => new PlayerGroupedByCountry
                    {
                        Country = g.Key,
                        Players = g.ToList()
                    })
                    .ToListAsync();
            }
        }
    }
}
