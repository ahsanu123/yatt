using LinqToDB.Identity;
using YATT.Libs.Attributes;

namespace YATT.Libs.Models;


public class IdentityRole : IdentityRole<long>
{
    [PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override long Id { get; set; }

    public IdentityRole() { }

    public IdentityRole(string roleName)
    {
        Name = roleName;
        NormalizedName = roleName.ToUpper();
    }
}

public class IdentityRoleClaim : IdentityRoleClaim<long>
{
    [PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override int Id { get; set; }

    [ForeignKey(typeof(IdentityRole), propName: nameof(IdentityRole.Id))]
    public override long RoleId { get; set; }
}

public class IdentityUser : IdentityUser<long>
{
    [PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override long Id { get; set; }

    public IdentityUser() { }

    public IdentityUser(string username, string email)
    {
        UserName = username;
        Email = email;
        NormalizedUserName = username.ToUpper();
        NormalizedEmail = email.ToLower();
    }
}

public class IdentityUserClaim : IdentityUserClaim<long>
{
    [PrimaryKey, LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
    public override int Id { get; set; }

    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }
}

public class IdentityUserLogin : IdentityUserLogin<long>
{
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }
}

public class IdentityUserRole : IdentityUserRole<long>
{
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }

    [ForeignKey(typeof(IdentityRole), propName: nameof(IdentityRole.Id))]
    public override long RoleId { get; set; }
}

public class IdentityUserToken : IdentityUserToken<long>
{
    [ForeignKey(typeof(IdentityUser), propName: nameof(IdentityUser.Id))]
    public override long UserId { get; set; }
}

