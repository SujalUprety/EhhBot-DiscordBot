using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using EhhhhBot.config;

namespace EhhhhBot;

public abstract class Program {
    
    private static DiscordClient _client { get; set; }
    private static CommandsNextExtension _commands { get; set; }

    public static async Task Main(string[] args) {
        var jsonReader = new JsonReader();
        await jsonReader.ReadJson();

        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.Guilds | DiscordIntents.GuildMessages | DiscordIntents.DirectMessages | DiscordIntents.MessageContents,
            Token = jsonReader.token,
            TokenType = TokenType.Bot,
            AutoReconnect = true
        };
        
        _client = new DiscordClient(discordConfig);

        _commands = _client.UseCommandsNext(new CommandsNextConfiguration()
        {
            StringPrefixes = new []{"!"},
            EnableMentionPrefix = true,
            EnableDms = true,
            EnableDefaultHelp = false
        });
        
        _commands.RegisterCommands<EhhCommand>();
        
        _client.SocketOpened += Client_Ready;

        await _client.ConnectAsync();
        await Task.Delay(-1);
    }
    
    private static Task Client_Ready(DiscordClient sender, SocketEventArgs e) {
        return Task.CompletedTask;
    }
    
}