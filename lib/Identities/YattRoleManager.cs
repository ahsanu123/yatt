using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using YattIdentityRole = YATT.Libs.Models.IdentityRole;

namespace YATT.Libs.Identities;

public class YattRoleManager : RoleManager<YattIdentityRole>
{
    public YattRoleManager(
        IRoleStore<YattIdentityRole> store,
        IEnumerable<IRoleValidator<YattIdentityRole>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<YattIdentityRole>> logger
    )
        : base(store, roleValidators, keyNormalizer, errors, logger) { }
}
