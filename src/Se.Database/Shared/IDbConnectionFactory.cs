using System.Data;

namespace Se.Database.Shared;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
