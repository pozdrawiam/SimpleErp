using System.Data;

namespace Se.Database.Shared;

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
