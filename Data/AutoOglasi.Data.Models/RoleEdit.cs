namespace AutoOglasi.Data.Models
{
    
    using static Common.DataConstants;

    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}
