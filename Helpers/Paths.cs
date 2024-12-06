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
        if (!ConfigPath.Exists)
        {
            Console.WriteLine("Config file not found at {0}.", ConfigPath.FullName);
            return false;
        }

        if (!TnsToolPath.Exists)
        {
            Console.WriteLine("TnsTool not found at {0}.", TnsToolPath.FullName);
            return false;
        }

        if (!DonderfulExtractorPath.Exists)
        {
            Console.WriteLine("DonderfulJSONExtractor not found at {0}.", DonderfulExtractorPath.FullName);
            return false;
        }

        if (!SteamMusicInfo.Exists)
        {
            Console.WriteLine("Steam musicinfo.bin not found at {0}.", SteamMusicInfo.FullName);
            return false;
        }

        if (!SteamInitialPossession.Exists)
        {
            Console.WriteLine("Steam initial_possession.bin not found at {0}.", SteamInitialPossession.FullName);
            return false;
        }

        if (!SwitchMusicData.Exists)
        {
            Console.WriteLine("Switch musicdata.unity3d not found at {0}.", SwitchMusicData.FullName);
            return false;
        }

        if (!SwitchWordData.Exists)
        {
            Console.WriteLine("Switch worddata.unity3d not found at {0}.", SwitchWordData.FullName);
            return false;
        }

        if (!SwitchCsv.Exists)
        {
            Console.WriteLine("Switch CSV directory not found at {0}.", SwitchCsv.FullName);
            return false;
        }

        if (!SwitchFumen.Exists)
        {
            Console.WriteLine("Switch Fumen directory not found at {0}.", SwitchFumen.FullName);
            return false;
        }

        if (!SwitchPresong.Exists)
        {
            Console.WriteLine("Switch Presong directory not found at {0}.", SwitchPresong.FullName);
            return false;
        }

        if (!SwitchSong.Exists)
        {
            Console.WriteLine("Switch Song directory not found at {0}.", SwitchSong.FullName);
            return false;
        }

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
            Console.WriteLine("Cleaning directory {0}.", directory.FullName);
        
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