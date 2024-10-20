﻿using System.ComponentModel.DataAnnotations;

namespace NET_Project_Server.Web.Models
{
    public class Client
    {
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Minimum 2 characters")]
        [Required]
        public string? Name { get; set; }

        [Required]
        public int? ID { get; set; }

        [Phone]
        public string? Phone { get; set; }
        public string? Country { get; set; }
    }
}
