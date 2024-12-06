using Newtonsoft.Json;

namespace TaikoSwitchConverter.DTO.Steam;

public class InitialPossession
{
    [JsonProperty("items")] 
    public List<InitialPossessionSingle> InitialPossessions { get; set; } = [];
}