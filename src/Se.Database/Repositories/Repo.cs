using System.Data;
using Se.Database.DbConnection;

namespace Se.Database.Repositories;

public abstract class Repo
{
    protected readonly IDbConnectionFactory ConnectionFactory;

    protected Repo(IDbConnectionFactory connectionFactory)
    {
        ConnectionFactory = connectionFactory;
    }
    
    protected IDbConnection CreateOpenConnection()
    {
        var dbConnection = ConnectionFactory.CreateConnection();
        dbConnection.Open();

        return dbConnection;
    }
}
