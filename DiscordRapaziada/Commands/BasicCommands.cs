using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using DSharpPlus.Interactivity;

namespace DiscordRapaziada.Commands
{
    public class BasicCommands : BaseCommandModule
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

        [Command("tuttao")]
        [Description("Informa quem é o tuttao.")]
        public async Task Tuttao(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Malucão").ConfigureAwait(false);
        }

        [Command("respondmessage")]
        public async Task RespondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("respondreaction")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }
    }
}