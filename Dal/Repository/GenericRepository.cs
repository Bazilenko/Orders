using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dal.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Dal.Repository
{ 
    public class GenericRepository<T> : IGenericRepository<T>
    {
        protected SqlConnection _dbConnection;
        private readonly string _tableName;
        protected IDbTransaction _dbTransaction;

        public GenericRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName)
        {
            _dbConnection = sqlConnection;
            _dbTransaction = dbTransaction;
            _tableName = tableName;
        }
        public async Task<int> AddAsync(T entity)
        {
            var insertQuery = GenerateInsertQuery();
            var newId = await _dbConnection.ExecuteScalarAsync<int>(insertQuery,
                param: entity,
                transaction: _dbTransaction);
            return newId;
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> items)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();
            inserted += await _dbConnection.ExecuteAsync(query,
                param: items);
            return inserted;
        }

        public async Task DeleteAsync(int id)
        {
            await _dbConnection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id = {id}",
                param: new { Id = id },
                transaction: _dbTransaction);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {_tableName}";
            return await _dbConnection.QueryAsync<T>(sql, transaction: _dbTransaction); ;
        }

        public async Task<T> GetAsync(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @id";
            var result = await _dbConnection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @id",
                param: new { Id = id },
                transaction: _dbTransaction);
            if (result == null)
                throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
            return result;
        }

        public async Task ReplaceAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery();
            await _dbConnection.ExecuteAsync(updateQuery,
                param: t,
                transaction: _dbTransaction);
        }
        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        //  INSERT
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
            insertQuery.Append("(");
            var properties = GenerateListOfProperties(GetProperties);
            properties.Remove("Id");
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");
            insertQuery.Append("; SELECT SCOPE_IDENTITY()");
            return insertQuery.ToString();
        }
        //  UPDATE
        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=@Id");
            return updateQuery.ToString();
        }
    }
}
