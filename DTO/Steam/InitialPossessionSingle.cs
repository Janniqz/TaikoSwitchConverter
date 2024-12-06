using Newtonsoft.Json;

namespace TaikoSwitchConverter.DTO.Steam;

public class InitialPossessionSingle
{
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; set; }
    [JsonProperty("id", Required = Required.Always)]
    public int ID { get; set; }
}