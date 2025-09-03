using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using YATT.Migrations.Extensions;
using YATT.Libs.Models;
using YattIdentityRole = YATT.Libs.Models.IdentityRole;
using YattIdentityUser = YATT.Libs.Models.IdentityUser;

namespace YATT.Tests.Migrations;

public class MigrationLinq2DbIdentityTest : MigrationBaseTest
{
    public MigrationLinq2DbIdentityTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
        : base(testFixture, testOutputHelper) { }

    [Fact]
    public void CheckUserStoreAndRoleStore()
    {
        var serviceProvider = GetServiceProvider();

        var userStore = serviceProvider.GetService<IUserStore<YattIdentityUser>>();
        var roleStore = serviceProvider.GetService<IRoleStore<YattIdentityRole>>();

        Assert.NotNull(userStore);
        Assert.NotNull(roleStore);
    }

    [Fact]
    public void InsertRole()
    {
        var serviceProvider = GetServiceProvider();

        var userStore = serviceProvider.GetService<IUserStore<YattIdentityUser>>();
        var roleStore = serviceProvider.GetService<IRoleStore<YattIdentityRole>>();

        Assert.NotNull(userStore);
        Assert.NotNull(roleStore);

        var identityRole = new YattIdentityRole(UserRoles.Employee.ToString());
        var alreadyExits =
            roleStore
                .FindByNameAsync(UserRoles.Employee.ToString().ToUpper(), CancellationToken.None)
                .GetValue() != null;

        if (!alreadyExits)
        {
            var result = roleStore.CreateAsync(identityRole, CancellationToken.None).GetValue();

            _output.SerializeObject(result);
        }
        else
            _output.WriteLine("Role Already Exists");
    }

    [Fact]
    public void InsertUser()
    {
        var serviceProvider = GetServiceProvider();

        var userStore = serviceProvider.GetService<IUserStore<YattIdentityUser>>();
        var roleStore = serviceProvider.GetService<IRoleStore<YattIdentityRole>>();

        Assert.NotNull(userStore);
        Assert.NotNull(roleStore);


        var userAlreadyExits = userStore.FindByNameAsync("Ahsanu".ToUpper(), CancellationToken.None).GetValue() != null;

        if (!userAlreadyExits)
        {
            var identityUser = new YattIdentityUser("Ahsanu", "caasperahsanuamala5@gmail.com");
            var result = userStore.CreateAsync(identityUser, CancellationToken.None).GetValue();

            _output.SerializeObject(result);
        }

        else
            _output.WriteLine("Username Already Exists");

    }

    [Fact]
    public void ListServices()
    {
        var serviceCollection = GetServiceCollection();

        foreach (var serviceDescriptor in serviceCollection)
        {
            _output.WriteLine(
                $@"- Service Type: {serviceDescriptor.ServiceType.FullName}, 
                    Lifetime: {serviceDescriptor.Lifetime}
                    Implementation: {serviceDescriptor.ImplementationType?.FullName ?? serviceDescriptor.ImplementationInstance?.GetType().FullName}"
            );
        }
    }
}
