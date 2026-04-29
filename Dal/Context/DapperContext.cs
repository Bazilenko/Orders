public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection 
            => _connection ??= new SqlConnection(_connectionString);

        public IDbTransaction Transaction => _transaction;

        public void BeginTransaction()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            _transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }