public interface IDapperContext : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }