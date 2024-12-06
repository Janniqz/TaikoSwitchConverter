using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace TaikoSwitchConverter.DTO.Steam;

public class MusicInfoSingle
{
    [JsonIgnore] private string FontJP => "<font=jp>";
    [JsonIgnore] private string FontEN => "<font=efigs>";
    [JsonIgnore] private string FontTW => "<font=tw>";
    [JsonIgnore] private string FontCN => "<font=cn>";
    [JsonIgnore] private string FontKO => "<font=ko>";
    
    [JsonProperty("UniqueId", Required = Required.Always)]
    public int UniqueID { get; set; }
    [JsonProperty("Id", Required = Required.Always)]
    public string ID { get; set; }
    [JsonProperty("SongFileName", Required = Required.Always)]
    public string SongFileName { get; set; }

    [JsonProperty("Session", Required = Required.Always)]
    public string Session { get; set; } = string.Empty;

    [JsonProperty("Order", Required = Required.Always)]
    public int Order { get; set; }

    [JsonProperty("GenreNo", Required = Required.Always)]
    public int Genre { get; set; }

    [JsonProperty("InPackage", Required = Required.Always)]
    public int InPackage { get; set; } = 1;
    [JsonProperty("HasPreviewInPackage", Required = Required.Always)]
    public bool HasPreviewInPackage { get; set; } = true;

    [JsonProperty("IsDefault", Required = Required.Always)]
    public bool IsDefault { get; set; } = true;
    [JsonProperty("CanMyBattleSong", Required = Required.Always)]
    public bool CanMyBattleSong { get; set; } = false;
    [JsonProperty("IsCap", Required = Required.Always)]
    public bool IsCap { get; set; } = true;
    [JsonProperty("IsOfflinePickUp", Required = Required.Always)]
    public bool IsOfflinePickUp { get; set; } = true;

    [JsonProperty("BranchEasy", Required = Required.Always)]
    public bool BranchEasy { get; set; }
    [JsonProperty("BranchNormal", Required = Required.Always)]
    public bool BranchNormal { get; set; }
    [JsonProperty("BranchHard", Required = Required.Always)]
    public bool BranchHard { get; set; }
    [JsonProperty("BranchMania", Required = Required.Always)]
    public bool BranchMania { get; set; }
    [JsonProperty("BranchUra", Required = Required.Always)]
    public bool BranchUra { get; set; }

    [JsonProperty("StarEasy", Required = Required.Always)]
    public int StarEasy { get; set; }
    [JsonProperty("StarNormal", Required = Required.Always)]
    public int StarNormal { get; set; }
    [JsonProperty("StarHard", Required = Required.Always)]
    public int StarHard { get; set; }
    [JsonProperty("StarMania", Required = Required.Always)]
    public int StarMania { get; set; }
    [JsonProperty("StarUra", Required = Required.Always)]
    public int StarUra { get; set; }

    [JsonProperty("ShinutiEasy", Required = Required.Always)]
    public int ShinutiEasy { get; set; }
    [JsonProperty("ShinutiNormal", Required = Required.Always)]
    public int ShinutiNormal { get; set; }
    [JsonProperty("ShinutiHard", Required = Required.Always)]
    public int ShinutiHard { get; set; }
    [JsonProperty("ShinutiMania", Required = Required.Always)]
    public int ShinutiMania { get; set; }
    [JsonProperty("ShinutiUra", Required = Required.Always)]
    public int ShinutiUra { get; set; }
    [JsonProperty("ShinutiEasyDuet", Required = Required.Always)]
    public int ShinutiEasyDuet { get; set; }
    [JsonProperty("ShinutiNormalDuet", Required = Required.Always)]
    public int ShinutiNormalDuet { get; set; }
    [JsonProperty("ShinutiHardDuet", Required = Required.Always)]
    public int ShinutiHardDuet { get; set; }
    [JsonProperty("ShinutiManiaDuet", Required = Required.Always)]
    public int ShinutiManiaDuet { get; set; }
    [JsonProperty("ShinutiUraDuet", Required = Required.Always)]
    public int ShinutiUraDuet { get; set; }

    [JsonProperty("Debug", Required = Required.Always)]
    public bool Debug { get; set; } = false;

    [JsonProperty("PlayableRegionList", Required = Required.Always)]
    public string PlayableRegionList { get; set; } = string.Empty;
    [JsonProperty("SubscriptionRegionList", Required = Required.Always)]
    public string SubscriptionRegionList { get; set; } = string.Empty;
    [JsonProperty("DlcRegionList", Required = Required.Always)]
    public string DlcRegionList { get; set; } = string.Empty;

    [JsonProperty("IsPS5ShareScreen", Required = Required.Always)]
    public bool IsPS5ShareScreen { get; set; } = false;

    [JsonProperty("Reserve2", Required = Required.Always)]
    public string Reserve2 { get; set; } = string.Empty;
    [JsonProperty("Reserve3", Required = Required.Always)]
    public string Reserve3 { get; set; } = string.Empty;

    [JsonIgnore] public string SongNameJP { get; set; }
    [JsonIgnore] public string SongNameEN { get; set; }
    [JsonIgnore] public string SongNameFR { get; set; }
    [JsonIgnore] public string SongNameIT { get; set; }
    [JsonIgnore] public string SongNameDE { get; set; }
    [JsonIgnore] public string SongNameES { get; set; }
    [JsonIgnore] public string SongNameTW { get; set; }
    [JsonIgnore] public string SongNameCN { get; set; }
    [JsonIgnore] public string SongNameKO { get; set; }

    [JsonProperty("IsDispJpSongName", Required = Required.Always)]
    public bool IsDispJpSongName { get; set; } = true;

    [JsonIgnore] public string SongSubJP { get; set; }
    [JsonIgnore] public string SongSubEN { get; set; }
    [JsonIgnore] public string SongSubFR { get; set; }
    [JsonIgnore] public string SongSubIT { get; set; }
    [JsonIgnore] public string SongSubDE { get; set; }
    [JsonIgnore] public string SongSubES { get; set; }
    [JsonIgnore] public string SongSubTW { get; set; }
    [JsonIgnore] public string SongSubCN { get; set; }
    [JsonIgnore] public string SongSubKO { get; set; }
    
    [JsonProperty("DancerSet", Required = Required.Always)]
    public string DancerSet { get; set; } = string.Empty;

    [JsonProperty("SongNameJP")] public string FullSongNameJP
    {
        get => $"{FontJP}{SongNameJP}";
        set => SongNameJP = value.Substring(9);
    }
    [JsonProperty("SongNameEN")] public string FullSongNameEN
    {
        get => $"{FontEN}{SongNameEN}";
        set => SongNameEN = value.Substring(12);
    }
    [JsonProperty("SongNameFR")] public string FullSongNameFR
    {
        get => $"{FontEN}{SongNameFR}";
        set => SongNameFR = value.Substring(12);
    }
    [JsonProperty("SongNameIT")] public string FullSongNameIT
    {
        get => $"{FontEN}{SongNameIT}";
        set => SongNameIT = value.Substring(12);
    }
    [JsonProperty("SongNameDE")] public string FullSongNameDE
    {
        get => $"{FontEN}{SongNameDE}";
        set => SongNameDE = value.Substring(12);
    }
    [JsonProperty("SongNameES")] public string FullSongNameES
    {
        get => $"{FontEN}{SongNameES}";
        set => SongNameES = value.Substring(12);
    }
    [JsonProperty("SongNameTW")] public string FullSongNameTW
    {
        get => $"{FontTW}{SongNameTW}";
        set => SongNameTW = value.Substring(9);
    }
    [JsonProperty("SongNameCN")] public string FullSongNameCN
    {
        get => $"{FontCN}{SongNameCN}";
        set => SongNameCN = value.Substring(9);
    }
    [JsonProperty("SongNameKO")] public string FullSongNameKO
    {
        get => $"{FontKO}{SongNameKO}";
        set => SongNameKO = value.Substring(9);
    }
    
    [JsonProperty("SongSubJP")] public string FullSongSubJP
    {
        get => $"{FontJP}{SongSubJP}";
        set => SongSubJP = value.Substring(9);
    }
    [JsonProperty("SongSubEN")] public string FullSongSubEN
    {
        get => $"{FontEN}{SongSubEN}";
        set => SongSubEN = value.Substring(12);
    }
    [JsonProperty("SongSubFR")] public string FullSongSubFR
    {
        get => $"{FontEN}{SongSubFR}";
        set => SongSubFR = value.Substring(12);
    }
    [JsonProperty("SongSubIT")] public string FullSongSubIT
    {
        get => $"{FontEN}{SongSubIT}";
        set => SongSubIT = value.Substring(12);
    }
    [JsonProperty("SongSubDE")] public string FullSongSubDE
    {
        get => $"{FontEN}{SongSubDE}";
        set => SongSubDE = value.Substring(12);
    }
    [JsonProperty("SongSubES")] public string FullSongSubES
    {
        get => $"{FontEN}{SongSubES}";
        set => SongSubES = value.Substring(12);
    }
    [JsonProperty("SongSubTW")] public string FullSongSubTW
    {
        get => $"{FontTW}{SongSubTW}";
        set => SongSubTW = value.Substring(9);
    }
    [JsonProperty("SongSubCN")] public string FullSongSubCN
    {
        get => $"{FontCN}{SongSubCN}";
        set => SongSubCN = value.Substring(9);
    }
    [JsonProperty("SongSubKO")] public string FullSongSubKO
    {
        get => $"{FontKO}{SongSubKO}";
        set => SongSubKO = value.Substring(9);
    }
}