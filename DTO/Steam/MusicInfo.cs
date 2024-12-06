using Newtonsoft.Json;

namespace TaikoSwitchConverter.DTO.Steam;

public class MusicInfo
{
    [JsonProperty("items")]
    public List<MusicInfoSingle> MusicInfos = [];
    
    public bool HasMusicInfo(string id) => MusicInfos.Any(info => info.ID == id);

    public bool HasID(int id) => MusicInfos.Any(info => info.UniqueID == id);
}