using Microsoft.AspNetCore.Identity;

namespace NSIProject.Domain.Entities;

public class ApplicationRole : IdentityRole<string>
{
    public IList<ApplicationUserRole> UserRoles { get; set; }
}