using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingAnalyser.Services;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient<IStarlingService, StarlingService>(
            client =>
            {
                client.BaseAddress = new Uri("https://api.starlingbank.com/api/v2/");
            });
    })
    .Build();

var configuration = host.Services.GetRequiredService<IConfiguration>();
var service = host.Services.GetRequiredService<IStarlingService>();
var accountToken = configuration["Starling:Account"];

if (string.IsNullOrEmpty(accountToken))
{
    Console.WriteLine("Error: account token is null or empty");
    return;
}

string accountUid = await service.GetAccountUri(accountToken);

if (string.IsNullOrEmpty(accountUid))
{
    Console.WriteLine("Error: accountUid is null or empty");
    return;
}

var statementToken = configuration["Starling:StatementCSV"];

if (string.IsNullOrEmpty(statementToken))
{
    Console.WriteLine("Error: account token is null or empty");
    return;
}

var statement = await service.GetStatement(statementToken, accountUid, "2024", "12");

foreach (var item in statement)
{
    Console.WriteLine($"Date: {item.Date}, Counter Party: {item.CounterParty}, Amount (GBP): {item.AmountGBP}, Balance (GBP): {item.BalanceGBP}, Spending Category: {item.SpendingCategory}");
}

Console.ReadLine();
