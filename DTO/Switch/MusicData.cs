using Newtonsoft.Json;

namespace TaikoSwitchConverter.DTO.Switch;

public class MusicData
{
    [JsonProperty("items")]
    public List<MusicDataSingle> MusicDatas = [];
}