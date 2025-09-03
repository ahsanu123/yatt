using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Identity;

using YattIdentityRole = YATT.Libs.Models.IdentityRole;
using YattIdentityUser = YATT.Libs.Models.IdentityUser;

namespace YATT.Libs.Databases;

public class YattIdentityDataConnection : IdentityDataConnection<YattIdentityUser, YattIdentityRole, long>
{
    public YattIdentityDataConnection(IDataProvider dataProvider, string connectionString)
        : base(dataProvider, connectionString) { }
}

public class YattDefaultConnectionFactory : IConnectionFactory
{
    protected string ConnectionString { get; set; }

    public YattDefaultConnectionFactory(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public DataConnection GetConnection()
    {
        var dataProvider = SqlServerTools.GetDataProvider(connectionString: ConnectionString);

        return new YattIdentityDataConnection(dataProvider, ConnectionString);
    }

    public IDataContext GetContext()
    {
        return new DataContext();
    }
}
