﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Pastel;
using SammBotNET.Database;
using SammBotNET.Services;
using System;
using System.Threading.Tasks;

namespace SammBotNET
{
    public partial class MainProg
    {
        public DiscordSocketClient SocketClient;
        public CommandService CommandService;

        public static void Main()
            => new MainProg().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            GlobalConfig.Instance.StartupStopwatch.Start();

            Console.WriteLine("Starting Socket Client...".Pastel("#3d9785"));

            SocketClient = new(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Warning,
                MessageCacheSize = 1000,
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.All
            });
            CommandService = new(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                DefaultRunMode = RunMode.Async,
            });

            Console.WriteLine("Configuring Services...".Pastel("#3d9785"));

            ServiceCollection services = new();
            ConfigureServices(services);

            ServiceProvider provider = services.BuildServiceProvider();
            provider.GetRequiredService<Logger>();
            provider.GetRequiredService<CMDHandler>();
            provider.GetRequiredService<CustomCommandService>();
            provider.GetRequiredService<HelpService>();
            provider.GetRequiredService<QuoteService>();
            provider.GetRequiredService<MathService>();
            provider.GetRequiredService<RandomService>();
            provider.GetRequiredService<AdminService>();
            provider.GetRequiredService<NsfwService>();

            Console.WriteLine("Starting Startup Service...".Pastel("#3d9785"));
            await provider.GetRequiredService<StartupService>().StartAsync();

            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(SocketClient)
            .AddSingleton(CommandService)
            .AddSingleton<CMDHandler>()
            .AddSingleton<StartupService>()
            .AddSingleton<Logger>()
            .AddSingleton<CustomCommandService>()
            .AddSingleton<HelpService>()
            .AddSingleton<Random>()
            .AddSingleton<QuoteService>()
            .AddSingleton<MathService>()
            .AddSingleton<RandomService>()
            .AddSingleton<AdminService>()
            .AddSingleton<NsfwService>();
        }
    }
}
