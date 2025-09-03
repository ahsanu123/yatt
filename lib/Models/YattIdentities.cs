using LinqToDB.Identity;
using LinqToDB.Mapping;
using YATT.Libs.Attributes;

namespace YATT.Libs.Models;

[Table(nameof(IdentityRole))]
public class IdentityRole : IdentityRole<long>
{
    [Column]
    [Attributes.PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override long Id { get; set; }

    [Column]
    public override string Name { get; set; }

    [Column]
    public override string NormalizedName { get; set; }

    [Column]
    public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    public IdentityRole() { }

    public IdentityRole(string roleName)
    {
        Name = roleName;
        NormalizedName = roleName.ToUpper();
    }
}

[Table(nameof(IdentityRoleClaim))]
public class IdentityRoleClaim : IdentityRoleClaim<long>
{
    [Column]
    [Attributes.PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override int Id { get; set; }

    [Column]
    [ForeignKey(typeof(IdentityRole), propName: nameof(IdentityRole.Id))]
    public override long RoleId { get; set; }

    [Column]
    public override string ClaimType { get; set; }

    [Column]
    public override string ClaimValue { get; set; }
}

[Table(nameof(IdentityUser))]
public class IdentityUser : IdentityUser<long>
{
    [Column]
    [Attributes.PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override long Id { get; set; }

    [Column]
    public override string NormalizedEmail { get; set; }

    [Column]
    public override string UserName { get; set; }

    [Column]
    public override string NormalizedUserName { get; set; }

    [Column]
    public override string Email { get; set; }

    [Column]
    public override bool EmailConfirmed { get; set; }

    [Column]
    public override string PasswordHash { get; set; }

    [Column]
    public override string SecurityStamp { get; set; }

    [Column]
    public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    [Column]
    public override string PhoneNumber { get; set; }

    [Column]
    public override bool PhoneNumberConfirmed { get; set; }

    [Column]
    public override bool TwoFactorEnabled { get; set; }

    [Column]
    public override DateTimeOffset? LockoutEnd { get; set; }

    [Column]
    public override bool LockoutEnabled { get; set; }

    [Column]
    public override int AccessFailedCount { get; set; }

    public IdentityUser()
    {
        SecurityStamp = Guid.NewGuid().ToString();
    }

    public IdentityUser(string username, string email)
    {
        UserName = username;
        Email = email;
        NormalizedUserName = username.ToUpper();
        NormalizedEmail = email.ToLower();
        SecurityStamp = Guid.NewGuid().ToString();
    }
}

[Table(nameof(IdentityUserClaim))]
public class IdentityUserClaim : IdentityUserClaim<long>
{
    [Column]
    [Attributes.PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override int Id { get; set; }

    [Column]
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }

    [Column]
    public override string ClaimType { get; set; }

    [Column]
    public override string ClaimValue { get; set; }
}

[Table(nameof(IdentityUserLogin))]
public class IdentityUserLogin : IdentityUserLogin<long>
{
    [Column]
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }

    [Column]
    public virtual string LoginProvider { get; set; }

    [Column]
    public virtual string ProviderKey { get; set; }

    [Column]
    public virtual string ProviderDisplayName { get; set; }

}

[Table(nameof(IdentityUserRole))]
public class IdentityUserRole : IdentityUserRole<long>
{
    [Column]
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }

    [Column]
    [ForeignKey(typeof(IdentityRole), propName: nameof(IdentityRole.Id))]
    public override long RoleId { get; set; }
}

[Table(nameof(IdentityUserToken))]
public class IdentityUserToken : IdentityUserToken<long>
{
    [Column]
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }

    [Column]
    public override string LoginProvider { get; set; }

    [Column]
    public override string Name { get; set; }

    [Column]
    public override string Value { get; set; }
}

