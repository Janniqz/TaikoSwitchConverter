using Newtonsoft.Json;

namespace TaikoSwitchConverter.DTO.Switch;

public class MusicDataSingle
{
    [JsonProperty("uniqueId", Required = Required.Always)]
    public int UniqueID { get; set; }
    [JsonProperty("id", Required = Required.Always)]
    public string ID { get; set; }
    [JsonProperty("songFileName", Required = Required.Always)]
    public string SongFileName { get; set; }
    
    [JsonProperty("order", Required = Required.Always)]
    public int Order { get; set; }
    [JsonProperty("genreNo", Required = Required.Always)]
    public int Genre { get; set; }
    
    [JsonProperty("debug")]
    public bool Debug { get; set; }

    [JsonProperty("branchEasy", Required = Required.Always)]
    public bool BranchEasy { get; set; }
    [JsonProperty("branchNormal", Required = Required.Always)]
    public bool BranchNormal { get; set; }
    [JsonProperty("branchHard", Required = Required.Always)]
    public bool BranchHard { get; set; }
    [JsonProperty("branchMania", Required = Required.Always)]
    public bool BranchMania { get; set; }
    [JsonProperty("branchUra", Required = Required.Always)]
    public bool BranchOni { get; set; }
    
    [JsonProperty("starEasy", Required = Required.Always)]
    public int StarEasy { get; set; }
    [JsonProperty("starNormal", Required = Required.Always)]
    public int StarNormal { get; set; }
    [JsonProperty("starHard", Required = Required.Always)]
    public int StarHard { get; set; }
    [JsonProperty("starMania", Required = Required.Always)]
    public int StarMania { get; set; }
    [JsonProperty("starUra", Required = Required.Always)]
    public int StarOni { get; set; }
    
    [JsonProperty("shinutiEasy", Required = Required.Always)]
    public int ShinutiEasy { get; set; }
    [JsonProperty("shinutiNormal", Required = Required.Always)]
    public int ShinutiNormal { get; set; }
    [JsonProperty("shinutiHard", Required = Required.Always)]
    public int ShinutiHard { get; set; }
    [JsonProperty("shinutiMania", Required = Required.Always)]
    public int ShinutiMania { get; set; }
    [JsonProperty("shinutiUra", Required = Required.Always)]
    public int ShinutiOni { get; set; }
    
    [JsonProperty("shinutiEasyDuet", Required = Required.Always)]
    public int ShinutiEasyDuet { get; set; }
    [JsonProperty("shinutiNormalDuet", Required = Required.Always)]
    public int ShinutiNormalDuet { get; set; }
    [JsonProperty("shinutiHardDuet", Required = Required.Always)]
    public int ShinutiHardDuet { get; set; }
    [JsonProperty("shinutiManiaDuet", Required = Required.Always)]
    public int ShinutiManiaDuet { get; set; }
    [JsonProperty("shinutiUraDuet", Required = Required.Always)]
    public int ShinutiOniDuet { get; set; }
}