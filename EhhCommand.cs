using System.Diagnostics;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace EhhhhBot;

public class EhhCommand : BaseCommandModule {

    [Command("ehh")]
    public async Task Ehh(CommandContext ctx) {
        await ctx.Channel.SendMessageAsync("Ehhh");
    }
    
    [Command("compile")]
    public async Task Compile(CommandContext ctx, [RemainingText] string code) {
        
        code = code.Replace("`", "");
        
        await File.WriteAllTextAsync("temp.ehh", code);

        
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                Arguments = "-c \"ehhmake temp.ehh\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        try{
            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            
            if (!string.IsNullOrWhiteSpace(output))
                await ctx.RespondAsync(output);

            await process.WaitForExitAsync();
        
            if (File.Exists("ehhmage.png")) {
                var image = new FileStream("ehhmage.png", FileMode.Open);
                
                var message = new DiscordMessageBuilder()
                    .WithContent("Here's your image!")
                    .AddFile("ehhmage.png", image);

                await ctx.RespondAsync(message);
                image.Close();
                
                File.Delete("ehhmage.png");
            }

            if (File.Exists("temp.ehh")) File.Delete("temp.ehh");
        } catch (Exception e) {
            Console.WriteLine(e);
        }

    }
    
}