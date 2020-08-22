using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordRapaziada.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("gordo")]
        [Description("Informa quem é o gordo oficial do grupo.")]
        public async Task Gordo(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Gordelazada da rapaziada.").ConfigureAwait(false);

        }

        [Command("soma")]
        [Description("Soma dois números.")]
        public async Task Soma(CommandContext ctx,
            [Description("Primeiro Numero")] int numberOne ,
            [Description("Segundo Numero")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);

        }
    }
}