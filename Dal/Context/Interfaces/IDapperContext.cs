using System.Data;

namespace Orders.Dal.Context.Interfaces{
public interface IDapperContext : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}