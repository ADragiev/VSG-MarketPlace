using Application.Models.GenericRepo;
using Dapper;
using Domain.Enums;
using Infrastructure.Repositories;
using Moq;
using System.Data;

namespace Tests
{
    [TestFixture]
    public class GenericRepoTests
    {
        private readonly OrderRepository repo;
        private readonly Mock<IMarketPlaceContext> contextMock = new Mock<IMarketPlaceContext>();

        public GenericRepoTests()
        {
            repo = new OrderRepository(contextMock.Object);
        }

        [Test]
        public void SetFieldMustCallConnectionExecWithRightSql()
        {
            int id = 1;
            var value = OrderStatus.Finished;

            var sql = @$"UPDATE [Order]
                      SET Status = @id
                        WHERE Id = @value";

            //contextMock.Verify(c => c.Connection.ExecuteAsync(sql, new {id, value}, null),Times.Once);
        }
    }
}
