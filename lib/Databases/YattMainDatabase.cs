using System.Data.Common;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using YATT.Libs.Models;

namespace YATT.Libs.Databases;

public class YattMainDatabase : DataConnection
{
    public YattMainDatabase(IDataProvider dataProvider, DbConnection connection)
        : base(dataProvider: dataProvider, connection: connection) { }

    public ITable<Event> Events => this.GetTable<Event>();
}
