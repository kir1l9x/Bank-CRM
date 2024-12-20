using DataAccess.Repositories;
using Interfaces.Repositories;
using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatform();
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddScoped<IUsersRepository, UsersRepository>();
        collection.AddScoped<IAccountsRepository, AccountsRepository>();
        collection.AddScoped<ITransactionsRepository, TransactionsRepository>();
        collection.AddScoped<IAuditLogRepository, AuditLogRepository>();
        collection.AddPlatformMigrations(typeof(DataAccessExtensions).Assembly);

        return collection;
    }
}