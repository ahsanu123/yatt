using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public static class IdentityStatic
{
    public static List<Type> AspnetCoreIdentityModel =>
        new List<Type>
        {
            typeof(IdentityRole),
            typeof(IdentityRoleClaim),
            typeof(IdentityUser),
            typeof(IdentityUserClaim),
            typeof(IdentityUserLogin),
            typeof(IdentityUserRole),
            typeof(IdentityUserToken),
        };
}
