using Newtonsoft.Json;

namespace EhhhhBot.config;

internal class JsonReader {
    
    public string token { get; set; }
    public string prefix { get; set; }
    
    public async Task ReadJson() {
        using (var sr = new StreamReader("./config/config.json")) {
            var json = sr.ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<JsonStructure>(json.Result);
            
            this.token = data.token;
            this.prefix = data.prefix;
        }
    }
    
}

internal sealed class JsonStructure {
    
    public string token { get; set; }
    public string prefix { get; set; }
    
}