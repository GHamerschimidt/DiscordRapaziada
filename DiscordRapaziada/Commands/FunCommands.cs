using System.Threading.Tasks;
using DSharpPlus.CommandsNext;

namespace DiscordRapaziada.Commands
{
    public class FunCommands : BaseCommandModule
    {
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong");
        }
    }
}