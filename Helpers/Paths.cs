namespace TaikoSwitchConverter.Helpers;

public static class Paths
{
    public static readonly FileInfo ConfigPath = new("./config.ini");
    public static readonly FileInfo TnsToolPath = new("./tns2tool/tns2tool.exe");
    public static readonly FileInfo DonderfulExtractorPath = new("./DonderfulJSONExtractor/DonderfulJSONExtractor.exe");
 
    public static readonly DirectoryInfo TempDirectory = new("./Temp");
    public static readonly DirectoryInfo FumenTempDirectory = new("./Temp/fumen");
    
    // Steam IN
    public static readonly FileInfo SteamMusicInfo = new("./IN/steam/musicinfo.bin");
    public static readonly FileInfo SteamMusicInfoJson = new("./IN/steam/musicinfo.json");
    public static readonly FileInfo SteamInitialPossession = new("./IN/steam/initial_possession.bin");
    public static readonly FileInfo SteamInitialPossessionJson = new("./IN/steam/initial_possession.json");
    
    // Steam Temp
    public static readonly FileInfo SteamMusicInfoJsonTemp = new("./Temp/musicinfo.json");
    public static readonly FileInfo SteamMusicInfoFinalTemp = new("./Temp/musicinfo.bin");
    public static readonly FileInfo SteamInitialPossessionJsonTemp = new("./Temp/initial_possession.json");
    public static readonly FileInfo SteamInitialPossessionFinalTemp = new("./Temp/initial_possession.bin");
    
    // Steam OUT
    public static readonly DirectoryInfo SteamCsvOut = new("./OUT/steam/fumencsv");
    public static readonly DirectoryInfo SteamFumenOut = new("./OUT/steam/fumen");
    public static readonly DirectoryInfo SteamSongOut = new("./OUT/steam/sound");
    
    public static readonly FileInfo SteamMusicInfoOut = new("./OUT/steam/ReadAssets/musicinfo.bin");
    public static readonly FileInfo SteamInitialPossessionOut = new("./OUT/steam/ReadAssets/initial_possession.bin");
    
    public static readonly DirectoryInfo SteamDataOut = new("./OUT/steam/ReadAssets");
    
    // Switch IN
    public static readonly FileInfo SwitchMusicData = new("./IN/switch/musicdata.unity3d");
    public static readonly FileInfo SwitchWordData = new("./IN/switch/worddata.unity3d");
    public static readonly FileInfo SwitchMusicDataJson = new("./IN/switch/musicdata.json");
    public static readonly FileInfo SwitchWordDataJson = new("./IN/switch/worddata.json");
    
    public static readonly DirectoryInfo SwitchCsv = new("./IN/switch/csv");
    public static readonly DirectoryInfo SwitchFumen = new("./IN/switch/fumen");
    public static readonly DirectoryInfo SwitchPresong = new("./IN/switch/presong");
    public static readonly DirectoryInfo SwitchSong = new("./IN/switch/song");
    
    public static bool CheckRequiredPaths()
    {
        if (!TnsToolPath.Exists)
            return false;
        if (!DonderfulExtractorPath.Exists)
            return false;
        
        if (!SteamMusicInfo.Exists)
            return false;
        if (!SteamInitialPossession.Exists)
            return false;
        
        if (!SwitchMusicData.Exists)
            return false;
        if (!SwitchWordData.Exists)
            return false;
        if (!SwitchCsv.Exists)
            return false;
        if (!SwitchFumen.Exists)
            return false;
        if (!SwitchPresong.Exists)
            return false;
        if (!SwitchSong.Exists)
            return false;

        CreateOrCleanDirectory(TempDirectory);
        CreateOrCleanDirectory(FumenTempDirectory);

        CreateOrCleanDirectory(SteamCsvOut);
        CreateOrCleanDirectory(SteamFumenOut);
        CreateOrCleanDirectory(SteamSongOut);
        CreateOrCleanDirectory(SteamDataOut);
        
        return true;
    }
    
    private static void CreateOrCleanDirectory(DirectoryInfo directory)
    {
        if (!directory.Exists)
            directory.Create();
        else
        {
            foreach (var file in directory.GetFiles())
                file.Delete();
            foreach (var subDirectory in directory.GetDirectories())
                subDirectory.Delete(true);
        }
    }
}