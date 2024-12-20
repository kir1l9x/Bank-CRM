using ConsoleUI.Scenarios.Admin;
using ConsoleUI.Scenarios.Admin.Changing;
using ConsoleUI.Scenarios.Admin.Creation;
using ConsoleUI.Scenarios.Admin.Deleting;
using ConsoleUI.Scenarios.Admin.Show;
using ConsoleUI.Scenarios.Client.Change;
using ConsoleUI.Scenarios.Client.Create;
using ConsoleUI.Scenarios.Client.Money;
using ConsoleUI.Scenarios.Client.Show;
using ConsoleUI.Scenarios.EntryScenario;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI.Extensions;

public static class ScenariosCollectionExtensions
{
    public static IServiceCollection AddConsoleScenarios(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, AuthenticateScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangeNameScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangePasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ReplenishScenarioProvider>();
        collection.AddScoped<IScenarioProvider, TransferScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowAccountsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowTransactionsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangeOwnNameScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangeOwnPasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangeUserNameScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangeUserPasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangeUserRoleScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateNewAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateNewUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DeleteAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DeleteUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, FindUserByIdScenarioProvider>();
        collection.AddScoped<IScenarioProvider, FindUserByUsernameScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetUserAccountsInfoScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowAccountTransactionsByDateScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowAccountTransactionsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowAllAccountsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowAllAuditLogsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowAllUsersScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowAuditLogsByDateScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowUserAuditLogsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, Scenarios.Client.Deleting.DeleteAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, Scenarios.Client.LogOutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogOutScenarioProvider>();

        return collection;
    }
}