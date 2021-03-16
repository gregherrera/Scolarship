using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Escolaridad.Data
{
    public class UserClaim: UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public UserClaim(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,  IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("NombreCompleto", user.FirstName + " " + user.LastName));
            identity.AddClaim(new Claim("UserId", user.Id.ToString()));
            return identity;
        }
    }
}
