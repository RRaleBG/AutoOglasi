using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOglasi.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FullName { get; set; }

        // Relative URL to the profile image stored in wwwroot (e.g. "/images/profiles/{userId}.jpg")
        public string ProfileImagePath { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
