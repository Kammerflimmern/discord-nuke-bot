using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace DiscordBotV1
{
    
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("ban")]
        [Summary("Bans a user from the server.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanAsync(IUser user, int pruneDays = 0, [Remainder] string reason = null)
        {
            if (user == null) await ReplyAsync("Please specify a user");
            else if (pruneDays == 0) await ReplyAsync("Please specify days to remove messages");
            else if (reason == null) await ReplyAsync("Please specify reason");
            else await Context.Guild.AddBanAsync(user, pruneDays, reason);
            await ReplyAsync("Banned" + user);
        }

        [Command("dump")]
        public async Task Dump()
        {
            var Admins = GuildPermission.Administrator;
            foreach (var users in Context.Guild.Users)
            {
                if (Context.Guild.Users is SocketGuild user)
                {
                    if (user.Roles.Equals(GuildPermission.Administrator)) user = Admins;
                }
                await ReplyAsync("Dumped:" + Admins);
            }
        }


        [Command("ping")]
        public async Task PingAsync()
        {
            await Context.Channel.SendMessageAsync("I am a dysfunctional bot that finally works");
        }
    }
}