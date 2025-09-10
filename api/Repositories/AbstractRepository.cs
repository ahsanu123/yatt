using LinqToDB;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.Data.SqlClient;
using YATT.Libs.Configs;
using YATT.Libs.Databases;
using YATT.Libs.Models;

namespace YATT.Api.Repositories;

public class AbstractRepository<T> : IAbstractRepository<T>
    where T : class, IBaseModel
{
    private IConfiguration _configuration { get; set; }

    // make another dataProvider
    // - MySqlDataProvider
    // - PosgresqlDataProvider
    // - etc...
    private static IDataProvider SqlServerDataProvider(string connectionString) =>
        SqlServerTools.GetDataProvider(connectionString: connectionString);

    protected async Task createDatabaseConnection(Func<YattMainDatabase, Task> databaseAction)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionStringConfig.YattDb);

        if (connectionString == null)
            throw new Exception("Cant Get Connection String");

        using var connection = new SqlConnection(connectionString);

        await connection.OpenAsync();
        var dataProvider = SqlServerDataProvider(connectionString);

        using var database = new YattMainDatabase(dataProvider, connection);

        await databaseAction(database);
    }

    public AbstractRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> CheckIfIdExists(long id)
    {
        var exist = false;

        await createDatabaseConnection(async db =>
        {
            var dataCount = await db.GetTable<T>().Where(pr => pr.Id == id).CountAsync();
            exist = dataCount > 0;
        });

        return exist;
    }

    public async Task<T?> GetById(long id)
    {
        T? data = null;

        await createDatabaseConnection(async db =>
        {
            data = await db.GetTable<T>().Where(pr => pr.Id == id).FirstOrDefaultAsync();
        });

        return data;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var datas = Enumerable.Empty<T>();

        await createDatabaseConnection(async db =>
        {
            datas = await db.GetTable<T>().ToListAsync();
        });

        return datas;
    }

    public async Task<long> Insert(T data)
    {
        var insertedCount = 0;

        await createDatabaseConnection(async db =>
        {
            insertedCount = await db.InsertAsync<T>(data);
        });

        return insertedCount;
    }

    public async Task<long> InsertAll(IEnumerable<T> datas)
    {
        var insertedCount = 0;

        await createDatabaseConnection(async db =>
        {
            foreach (var data in datas)
            {
                insertedCount += await db.InsertAsync<T>(data);
            }
        });

        return insertedCount;
    }

    public async Task<long> Update(T data)
    {
        var updatedCount = 0;

        await createDatabaseConnection(async db =>
        {
            updatedCount = await db.UpdateAsync<T>(data);
        });

        return updatedCount;
    }

    public async Task<long> UpdateAll(IEnumerable<T> datas)
    {
        var updatedCount = 0;

        await createDatabaseConnection(async db =>
        {
            foreach (var data in datas)
            {
                updatedCount += await db.UpdateAsync<T>(data);
            }
        });

        return updatedCount;
    }

    public async Task<long> Delete(int id)
    {
        var deletedCount = 0;

        await createDatabaseConnection(async db =>
        {
            deletedCount += await db.GetTable<T>().Where(pr => pr.Id == id).DeleteAsync();
        });

        return deletedCount;
    }

    public async Task<long> Count()
    {
        var count = 0;

        await createDatabaseConnection(async db =>
        {
            count += await db.GetTable<T>().CountAsync();
        });

        return count;
    }

    public async Task<IEnumerable<T>> GetPaged(int index, int pageSize)
    {
        var datas = Enumerable.Empty<T>();

        await createDatabaseConnection(async db =>
        {
            datas = await db.GetTable<T>()
                .Skip((index - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        });

        return datas;
    }
}
