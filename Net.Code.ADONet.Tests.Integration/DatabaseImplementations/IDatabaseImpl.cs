using System.Collections.Generic;
using Net.Code.ADONet.Extensions;

namespace Net.Code.ADONet.Tests.Integration
{
    public interface IDatabaseImpl
    {
        string CreatePersonTable { get; }
        string DropPersonTable { get; }
        string CreateAddressTable { get; }
        string DropAddressTable { get; }
        string InsertPerson { get; }
        string InsertAddress { get; }
        bool SupportsMultipleResultSets { get; }
        string ProviderName { get; }
        bool SupportsTableValuedParameters { get; }
        string EscapeChar { get; }
        void DropAndRecreate();
        IDb CreateDb();
        Person Project(dynamic d);
        MultiResultSet<Person, Address> SelectPersonAndAddress(IDb db);
        void BulkInsert(IDb db, IEnumerable<Person> list);
        IQueryGenerator Query<T>();
    }
}