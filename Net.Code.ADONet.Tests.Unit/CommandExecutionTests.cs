using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Net.Code.ADONet.Tests.Unit
{
    [TestFixture]
    public class CommandExecutionTests
    {
        [Test]
        public void AsEnumerable_WhenCalled_ReturnsResults()
        {
            var command = PrepareCommand();

            var commandBuilder = new CommandBuilder(command);

            command.SetResultSet(Person.GetSingleResultSet());

            var result = commandBuilder.AsEnumerable().ToList();

            Person.VerifySingleResultSet(result);
        }


        [Test]
        public void AsScalar_WhenCalled_ReturnsScalarValue()
        {
            var command = PrepareCommand();
            command.SetScalarValue(1);

            var commandBuilder = new CommandBuilder(command);

            var result = commandBuilder.AsScalar<int>();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void AsNonQuery_WhenCalled_ReturnsNonQueryResult()
        {
            var command = PrepareCommand();
            command.SetNonQueryResult(1);
            var commandBuilder = new CommandBuilder(command);

            var result = commandBuilder.AsNonQuery();
            
            Assert.AreEqual(1, result);
        }
        [Test]
        public void AsMultiResultSet_WhenCalled_ReturnsMultipleResultSets()
        {
            var command = PrepareCommand();

            var data = Person.GetMultiResultSet();

            command.SetMultiResultSet(data);
            var commandBuilder = new CommandBuilder(command);
            
            var result = commandBuilder.AsMultiResultSet().Select(x => x.ToList()).ToList();

            Person.VerifyMultiResultSet(result);
        }

        [Test]
        public async Task AsEnumerableAsync_WhenCalledAndAwaited_ReturnsResultSet()
        {
            var command = PrepareCommand();
            var commandBuilder = new CommandBuilder(command);
            command.SetResultSet(Person.GetSingleResultSet());

            var result = (await commandBuilder.AsEnumerableAsync()).ToList();

            Person.VerifySingleResultSet(result);
        }

        [Test]
        public async Task AsScalarAsync_WhenCalledAndAwaited_ReturnsScalarValue()
        {
            var command = PrepareCommand();
            command.SetScalarValue(1);
            var commandBuilder = new CommandBuilder(command);

            var result = await commandBuilder.AsScalarAsync<int>();

            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task AsNonQueryAsync_WhenCalledAndAwaited_ReturnsNonQueryValue()
        {
            var command = PrepareCommand();
            command.SetNonQueryResult(1);
            var commandBuilder = new CommandBuilder(command);
            
            var result = await commandBuilder.AsNonQueryAsync();
            
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task AsMultipleResultSetAsync_WhenCalledAndAwaited_ReturnsMultiResultSet()
        {
            var command = PrepareCommand();
            command.SetMultiResultSet(Person.GetMultiResultSet());
            var commandBuilder = new CommandBuilder(command);
            
            var result = (await commandBuilder.AsMultiResultSetAsync()).Select(x => x.ToList()).ToList();

            Person.VerifyMultiResultSet(result);
        }

        private static FakeCommand PrepareCommand()
        {
            var command = new FakeCommand(new FakeConnection());
            return command;
        }
    }
}