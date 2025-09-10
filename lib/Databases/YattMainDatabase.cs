using System.Data.Common;
using LinqToDB.Data;
using LinqToDB.DataProvider;

namespace YATT.Libs.Databases;

public class YattMainDatabase : DataConnection
{
    public YattMainDatabase(IDataProvider dataProvider, DbConnection connection)
        : base(dataProvider: dataProvider, connection: connection) { }
}
