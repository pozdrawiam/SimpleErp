using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Reflection;
using System.Text;
using System.Transactions;
using Se.Application.Base.Database;
using Se.Application.Base.Database.GetAll;
using Se.Database.DbConnection;

namespace Se.Database.Repositories;

public class CrudRepo<TEntity> : ICrudRepo<TEntity>
    where TEntity : class
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public CrudRepo(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<TEntity?> GetAsync(int id)
    {
        using var connection = CreateOpenConnection();

        var entity = await connection.GetAsync<TEntity>(id);

        return entity;
    }

    public async Task<GetAllResultDto> GetAllAsync(GetAllArgsDto query)
    {
        using var connection = CreateOpenConnection();

        var tableName = GetTableName();
        var availableColumns = typeof(TEntity)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(property => property.Name).ToArray();

        var selectedColumns = query.Columns.Length > 0 ? query.Columns.Where(x => availableColumns.Contains(x)).ToArray() : availableColumns;

        var sqlBuilder = new StringBuilder("SELECT ");
        sqlBuilder.Append(string.Join(", ", selectedColumns.Select(x => $"[{x}]")));
        sqlBuilder.Append($" FROM [{tableName}]");

        var selectedFilters = query.Filters.Where(x => availableColumns.Contains(x.Column)).ToArray();

        if (selectedFilters.Length > 0)
        {
            sqlBuilder.Append(" WHERE ");
            sqlBuilder.Append(string.Join(" AND ", selectedFilters.Select((filter, index) =>
            {
                var paramName = $"@param{index}";
                return filter.Operator switch
                {
                    GetAllFilterOperatorType.Equals => $"[{filter.Column}] = {paramName}",
                    GetAllFilterOperatorType.NotEquals => $"[{filter.Column}] <> {paramName}",
                    GetAllFilterOperatorType.GreaterThan => $"[{filter.Column}] > {paramName}",
                    GetAllFilterOperatorType.GreaterThanOrEqual => $"[{filter.Column}] >= {paramName}",
                    GetAllFilterOperatorType.LessThan => $"[{filter.Column}] < {paramName}",
                    GetAllFilterOperatorType.LessThanOrEqual => $"[{filter.Column}] <= {paramName}",
                    GetAllFilterOperatorType.Empty => $"[{filter.Column}] IS NULL",
                    GetAllFilterOperatorType.NotEmpty => $"[{filter.Column}] IS NOT NULL",
                    _ => throw new NotSupportedException($"Operator {filter.Operator} is not supported")
                };
            })));
        }

        var sortBy = !string.IsNullOrEmpty(query.SortBy) && availableColumns.Contains(query.SortBy) ?
            availableColumns.First(x => x == query.SortBy) :
            "Id";
        
        sqlBuilder.Append
        (
            $" ORDER BY [{sortBy}] {(query.SortDesc ? "DESC" : "ASC")} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"
        );

        var parameters = new DynamicParameters();
        for (int i = 0; i < selectedFilters.Length; i++)
        {
            parameters.Add($"@param{i}", query.Filters[i].Value);
        }

        parameters.Add("@Offset", (query.PageNumber - 1) * query.PageSize);
        parameters.Add("@PageSize", query.PageSize);

        var data = await connection.QueryAsync(sqlBuilder.ToString(), parameters);

        object?[][] result = data.Select(x =>
        {
            var row = (IDictionary<string, object>)x;
            object?[] values = selectedColumns.Select(column => row[column]).ToArray();
            return values;
        }).ToArray();

        return new GetAllResultDto(result, 0);
    }

    public async Task<int> AddAsync(TEntity entity)
    {
        using var connection = CreateOpenConnection();
        var id = await connection.InsertAsync(entity);

        return id;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        using var connection = CreateOpenConnection();
        await connection.UpdateAsync(entity);
    }

    public async Task DeleteManyAsync(IReadOnlyCollection<int> ids)
    {
        using var connection = CreateOpenConnection();
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        foreach (var id in ids)
        {
            var entity = await connection.GetAsync<TEntity>(id);

            if (entity != null)
            {
                await connection.DeleteAsync(entity);
            }
        }

        transactionScope.Complete();
    }

    private IDbConnection CreateOpenConnection()
    {
        var dbConnection = _dbConnectionFactory.CreateConnection();
        dbConnection.Open();

        return dbConnection;
    }
    
    private static string GetTableName()
    {
        var type = typeof(TEntity);
        
        var tableAttribute = type
            .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), inherit: false)
            .FirstOrDefault() as System.ComponentModel.DataAnnotations.Schema.TableAttribute;
        
        var tableName = tableAttribute?.Name;
        
        return tableName ?? throw new InvalidOperationException($"No table name for type {type.FullName}");
    }
}
