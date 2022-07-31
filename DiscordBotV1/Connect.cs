using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Discord.API;
using Discord.Rest;
using MemberInfo = System.Reflection.MemberInfo;

namespace DiscordBotV1
{
    public class Program
    {
        public DiscordSocketClient Client;
        public Commandhandler Commandhandler;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Client = new DiscordSocketClient();

            Client.Log += Log;
            var token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();
            Commandhandler = new Commandhandler(Client, new CommandService());
            await Commandhandler.InstallCommandsAsync();
            await Task.Delay(-1);
        }


        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}