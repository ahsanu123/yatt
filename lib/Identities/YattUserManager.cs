using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YattIdentityUser = YATT.Libs.Models.IdentityUser;

namespace YATT.Libs.Identities;

public class YattUserManager : UserManager<YattIdentityUser>
{
    public YattUserManager(
        IUserStore<YattIdentityUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<YattIdentityUser> passwordHasher,
        IEnumerable<IUserValidator<YattIdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<YattIdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<YattIdentityUser>> logger
    )
        : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger
        )
    { }
}
