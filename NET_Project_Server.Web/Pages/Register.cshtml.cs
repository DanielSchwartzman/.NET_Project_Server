using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NET_Project_Server.Web.Models;
using System;

namespace NET_Project_Server.Web.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {

        }


        [BindProperty]
        public Client client { get; set; } = default!;

        public void OnPost()
        {
            List<string?> list = new List<string?>() { client.Name, client.ID, client.Phone, client.Country };

        }
    }
}
