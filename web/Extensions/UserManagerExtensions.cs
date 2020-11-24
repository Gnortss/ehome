using System;
using System.Collections.Generic;
using System.Linq;
using web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace web.Extensions
{
    public static class UserManagerExtensions
    {
        public static string GetImageLink(this UserManager<ApplicationUser> um, string name)
        {
            return um?.Users?.Where(x => x.UserName == name).Select(x => x.ImageLink).Single();
        }

        public static async Task<IdentityResult> SetImageLinkAsync(this UserManager<ApplicationUser> um, ApplicationUser user, string imageLink)
        {
            user.ImageLink = imageLink;
            return await um?.UpdateAsync(user);
        }
    }
}