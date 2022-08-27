﻿namespace Befer.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        public IEnumerable<Post> Posts { get; } = new HashSet<Post>();
    }
}