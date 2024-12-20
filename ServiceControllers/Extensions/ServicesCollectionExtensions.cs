using Microsoft.Extensions.DependencyInjection;
using Services.Accounts;
using Services.AuditLogs;
using Services.Controllers;
using Services.Transactions;
using Services.Users;

namespace ServiceControllers.Extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddControllers(this IServiceCollection collection)
    {
        collection.AddScoped<INonAuthorizedController, UserController>();
        collection.AddScoped<IClientController, UserController>();
        collection.AddScoped<IAdminController, UserController>();
        collection.AddScoped<CurrentUserService>();
        collection.AddScoped<ICurrentUserService>(p => p.GetRequiredService<CurrentUserService>());
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<ITransactionService, TransactionService>();
        collection.AddScoped<IAuditLogService, AuditLogService>();

        return collection;
    }
}