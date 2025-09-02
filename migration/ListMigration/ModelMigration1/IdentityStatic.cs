using Microsoft.AspNetCore.Identity;
using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

class IdentityRole : IdentityRole<long>
{
    [PrimaryKey]
    public override long Id { get; set; }
}

class IdentityRoleClaim : IdentityRoleClaim<long>
{
    [PrimaryKey]
    public override int Id { get; set; }

    [ForeignKey(typeof(IdentityRole), propName: nameof(IdentityRole.Id))]
    public override long RoleId { get; set; }
}

class IdentityUser : IdentityUser<long>
{
    [PrimaryKey]
    public override long Id { get; set; }
}

class IdentityUserClaim : IdentityUserClaim<long>
{
    [PrimaryKey]
    public override int Id { get; set; }

    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }
}

class IdentityUserLogin : IdentityUserLogin<long>
{
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }
}

class IdentityUserRole : IdentityUserRole<long>
{
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }

    [ForeignKey(typeof(IdentityRole), propName: nameof(IdentityRole.Id))]
    public override long RoleId { get; set; }
}

class IdentityUserToken : IdentityUserToken<long>
{
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }
}

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
