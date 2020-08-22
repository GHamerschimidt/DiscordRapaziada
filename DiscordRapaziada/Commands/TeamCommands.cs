using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace DiscordRapaziada.Commands
{
    public class TeamCommands : BaseCommandModule
    {
        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            // Workaround for the thumbnail image
            DiscordEmbedBuilder.EmbedThumbnail thumbnailWorkAround = new DiscordEmbedBuilder.EmbedThumbnail();
            thumbnailWorkAround.Url = ctx.Client.CurrentUser.AvatarUrl;

            // Creates new embed
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Gostaria de se tornar um servo do bot?",
                Thumbnail = thumbnailWorkAround,
                Color = DiscordColor.Green,
            };

            // Prompts message for the user
            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            // Declaring the emojis used
            var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            // Getting response from user
            var reactionResult = await interactivity
                .WaitForReactionAsync(x =>
                    x.Message == joinMessage &&
                    x.User == ctx.User && 
                    (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji))
                .ConfigureAwait(false);


            // Bot slaves' role
            var role = ctx.Guild.GetRole(746764586807722006);

            if (reactionResult.Result.Emoji == thumbsUpEmoji)
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            else if (reactionResult.Result.Emoji == thumbsDownEmoji)
                await ctx.Member.RevokeRoleAsync(role).ConfigureAwait(false);


            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }
    }
}