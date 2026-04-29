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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IDapperContext _context;
        private readonly string _tableName;

        public GenericRepository(IDapperContext context, string tableName)
        {
            _context = context;
            _tableName = tableName;
        }

        public async Task<int> AddAsync(T entity)
        {
            var insertQuery = GenerateInsertQuery();
            return await _context.Connection.ExecuteScalarAsync<int>(insertQuery,
                param: entity,
                transaction: _context.Transaction);
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> items)
        {
            var query = GenerateInsertQuery();
            return await _context.Connection.ExecuteAsync(query,
                param: items,
                transaction: _context.Transaction);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
            await _context.Connection.ExecuteAsync(sql,
                param: new { Id = id },
                transaction: _context.Transaction);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {_tableName}";
            return await _context.Connection.QueryAsync<T>(sql, 
                transaction: _context.Transaction);
        }

        public async Task<T> GetAsync(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            var result = await _context.Connection.QuerySingleOrDefaultAsync<T>(sql,
                param: new { Id = id },
                transaction: _context.Transaction);

            if (result == null)
                throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
            
            return result;
        }

        public async Task ReplaceAsync(T entity)
        {
            var updateQuery = GenerateUpdateQuery();
            await _context.Connection.ExecuteAsync(updateQuery,
                param: entity,
                transaction: _context.Transaction);
        }


        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore")
                          && prop.Name != "Id"
                          && (prop.PropertyType == typeof(string) || !typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType))
                    select prop.Name).ToList();
        }

        private string GenerateInsertQuery()
        {
            var properties = GenerateListOfProperties(GetProperties);
            var columns = string.Join(", ", properties.Select(p => $"[{p}]"));
            var values = string.Join(", ", properties.Select(p => $"@{p}"));

            return $"INSERT INTO {_tableName} ({columns}) VALUES ({values}); SELECT SCOPE_IDENTITY();";
        }

        private string GenerateUpdateQuery()
        {
            var properties = GenerateListOfProperties(GetProperties);
            var setClause = string.Join(", ", properties.Select(p => $"{p}=@{p}"));

            return $"UPDATE {_tableName} SET {setClause} WHERE Id=@Id";
        }
    }
}
