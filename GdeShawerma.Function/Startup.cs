using System;
using GdeShawerma.Core;
using GdeShawerma.Core.Handlers;
using GdeShawerma.Db;
using GdeShawerma.Function;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(Startup))]

namespace GdeShawerma.Function;

public class Startup : FunctionsStartup
{
    public Startup()
    {
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        IConfiguration configuration = builder.GetContext().Configuration;

        builder.Services.AddMediatR(typeof(UpdateRequest));
        
        string token = configuration.GetValue<string>("BotToken");
        builder.Services.AddHttpClient("tgwebhook")
            .AddTypedClient<ITelegramBotClient>(httpClient
                => new TelegramBotClient(token, httpClient));

        builder.Services.Configure<Config>(configuration);

        builder.Services.AddHttpClient("Shawerma", client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("ShawermaBaseUrl"));
        });

        string connectionString = configuration.GetConnectionString("Cosmos");
        string dbName = configuration.GetConnectionString("DbName");
        builder.Services.AddDbContext<TgBotContext>(options =>
        {
            options.UseCosmos(
                connectionString: connectionString,
                databaseName: dbName);
        });
    }
}