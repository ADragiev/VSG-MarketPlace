
using System.Data;

namespace Application.Models.GenericRepo
{
    public interface IMarketPlaceContext : IDisposable
    {
        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; }
    }
}
