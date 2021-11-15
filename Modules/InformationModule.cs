﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SammBotNET.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SammBotNET.Modules
{
    [Name("Information")]
    [Summary("Bot information and statistics.")]
    [Group("info")]
    public class InformationModule : ModuleBase<SocketCommandContext>
    {
        [Command("full", RunMode = RunMode.Async)]
        [Summary("Shows the FULL information of the bot.")]
        public async Task<RuntimeResult> InformationFullAsync()
        {
            EmbedBuilder embed = new EmbedBuilder().BuildDefaultEmbed(Context, "Information", "All public information about the bot.");

            string elapsedTime = string.Format("{0:00}d{1:00}h{2:00}m",
                GlobalConfig.Instance.RuntimeStopwatch.Elapsed.Days,
                GlobalConfig.Instance.RuntimeStopwatch.Elapsed.Hours,
                GlobalConfig.Instance.RuntimeStopwatch.Elapsed.Minutes);

            embed.AddField("Bot Version", $"`{GlobalConfig.Instance.LoadedConfig.BotVersion}`", true);
            embed.AddField(".NET Version", $"`{RuntimeInformation.FrameworkDescription}`", true);
            embed.AddField("Ping", $"`{Context.Client.Latency}ms.`", true);
            embed.AddField("Im In", $"`{Context.Client.Guilds.Count} server/s.`", true);
            embed.AddField("Uptime", $"`{elapsedTime}`", true);
            embed.AddField("Host", $"`{FriendlyOSName()}`", true);

            await Context.Channel.SendMessageAsync("", false, embed.Build());

            return ExecutionResult.Succesful();
        }

        [Command("servers", RunMode = RunMode.Async)]
        [Alias("guilds")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [Summary("Shows a list of all the servers the bot is in.")]
        public async Task<RuntimeResult> ServersAsync()
        {
            string builtMsg = "I am invited in the following servers:\n```\n";
            string inside = string.Empty;

            int i = 1;
            foreach (SocketGuild guild in Context.Client.Guilds)
            {
                inside += $"{i}. {guild.Name} ({guild.Id}) with {guild.MemberCount} members.\n";
                i++;
            }
            inside += "```";
            builtMsg += inside;
            await ReplyAsync(builtMsg);

            return ExecutionResult.Succesful();
        }

        [Command("userinfo")]
        [Alias("user")]
        [MustRunInGuild]
        [Summary("Get information about a user!")]
        public async Task<RuntimeResult> UserInfoAsync(SocketGuildUser User)
        {
            string userAvatarUrl = User.GetAvatarUrl() ?? User.GetDefaultAvatarUrl();
            string userName = $"{User.Username}";
            string userDiscriminator = $"#{User.DiscriminatorValue}";
            string nickName = User.Nickname ?? "None";
            string isABot = User.IsBot.ToYesNo();
            string isAWebhook = User.IsWebhook.ToYesNo();
            string joinDate = $"<t:{User.JoinedAt.Value.ToUnixTimeSeconds()}>";
            string createDate = $"<t:{User.CreatedAt.ToUnixTimeSeconds()}>";
            string boostingSince = User.PremiumSince != null ? $"<t:{User.PremiumSince.Value.ToUnixTimeSeconds()}:R>" : "Never";
            string userRoles = User.Roles.Count > 1 ? 
                string.Join(", ", User.Roles.Skip(1).Select(x => $"<@&{x.Id}>")).Truncate(512)
                : "None";
            string userStatus = "Unknown";

            switch(User.Status)
            {
                case UserStatus.DoNotDisturb:   userStatus = "Do Not Disturb";  break;
                case UserStatus.Idle:           userStatus = "Idle";            break;
                case UserStatus.Offline:        userStatus = "Offline";         break;
                case UserStatus.Online:         userStatus = "Online";          break;
            }

            EmbedBuilder embed = new EmbedBuilder().BuildDefaultEmbed(Context).ChangeTitle("USER INFORMATION");

            embed.WithThumbnailUrl(userAvatarUrl);
            embed.AddField("Username", userName, true);
            embed.AddField("Nickname", nickName, true);
            embed.AddField("Discriminator", userDiscriminator, true);
            embed.AddField("Status", userStatus, true);
            embed.AddField("Is Bot", isABot, true);
            embed.AddField("Is Webhook", isAWebhook, true);
            embed.AddField("Join Date", joinDate, true);
            embed.AddField("Create Date", createDate, true);
            embed.AddField("Booster Since", boostingSince, true);
            embed.AddField("Roles", userRoles, false);

            await Context.Channel.SendMessageAsync(null, false, embed.Build());

            return ExecutionResult.Succesful();
        }

        public string FriendlyOSName()
        {
            string osName = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Version version = Environment.OSVersion.Version;

                switch (version.Major)
                {
                    case 6:
                        osName = version.Minor switch
                        {
                            1 => "Windows 7",
                            2 => "Windows 8",
                            3 => "Windows 8.1",
                            _ => "Unknown Windows",
                        };
                        break;
                    case 10:
                        switch (version.Minor)
                        {
                            case 0:
                                if (version.Build >= 22000) osName = "Windows 11";
                                else osName = "Windows 10";

                                break;
                            default: osName = "Unknown Windows"; break;
                        }
                        break;
                    default:
                        osName = "Unknown Windows";
                        break;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if (File.Exists("/etc/issue.net"))
                    osName = File.ReadAllText("/etc/issue.net");
                else
                    osName = "Linux";
            }

            return osName;
        }
    }
}