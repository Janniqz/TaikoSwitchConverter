using Newtonsoft.Json;

namespace TaikoSwitchConverter.DTO.Switch;

public class WordData
{
    [JsonProperty("items")]
    public List<WordDataSingle> WordDatas = [];
    
    public WordDataSingle? GetSongWordData(string id) => WordDatas.FirstOrDefault(data => data.Key == "song_" + id);
    public WordDataSingle? GetSongSubtitleWordData(string id) => WordDatas.FirstOrDefault(data => data.Key == "song_sub_" + id);
}