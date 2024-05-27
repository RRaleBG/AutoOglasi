using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoOglasi.Data.Models
{
    internal class RoleUserHelper
    {
        [HtmlTargetElement("td", Attributes = "i-role")]
        public class RoleUsersTH : ITagHelper
        {
            private UserManager<ApplicationUser> userManager;
            private RoleManager<IdentityRole> roleManager;

            public RoleUsersTH(UserManager<ApplicationUser> usermgr, RoleManager<IdentityRole> rolemgr)
            {
                userManager = usermgr;
                roleManager = rolemgr;
            }

            [HtmlAttributeName("i-role")]
            public string Role { get; set; }

            public int Order => throw new System.NotImplementedException();

            public void Init(TagHelperContext context)
            {
                throw new System.NotImplementedException();
            }

            public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
            {
                List<string> names = new List<string>();

                IdentityRole role = await roleManager.FindByIdAsync(Role);

                if (role != null)
                {
                    foreach (var user in userManager.Users)
                    {
                        if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                            names.Add(user.UserName);
                    }
                }
                output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
            }
        }
    }
}
