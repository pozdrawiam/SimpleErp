using System.Data;

namespace Se.Database.DbConnection;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
