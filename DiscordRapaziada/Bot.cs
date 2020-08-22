using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DiscordRapaziada.Commands;

namespace DiscordRapaziada
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }

        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync() 
            //Task is what you use instead of "void" in async stuff
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json); 
            // Converts the json to something the C# can understand using ConfigJson struct.

            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new[] {configJson.Prefix},
                EnableDms = false,
                EnableMentionPrefix = true, // instead of the prefix you can actually just call the bot at Bot "command"
                DmHelp = true,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<FunCommands>();

            await Client.ConnectAsync();

            await Task.Delay(-1); // Do whatever it has to do before quitting accidentally. 
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}