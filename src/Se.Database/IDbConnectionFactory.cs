using System.Data;

namespace Se.Database;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
