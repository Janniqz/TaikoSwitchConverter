using System.Text;
using Newtonsoft.Json;
using SharpConfig;
using TaikoSwitchConverter.DTO.Steam;
using TaikoSwitchConverter.DTO.Switch;
using TaikoSwitchConverter.Helpers;

namespace TaikoSwitchConverter;

class Program
{
    // Steam
    private static MusicInfo _musicInfos = new();
    private static InitialPossession _initialPossessions = new();
    
    // Switch
    private static MusicData _musicData = new();
    private static WordData _wordData = new();

    private static List<string> _availableSongs = [];
    private static List<Tuple<string, string>> _ignoredSongs = [];
    
    private static int _startingUniqueID = -1;
    private static int _maxUniqueID = -1;
    private static List<string> _excludedSongs = [];
    
    public static bool Quiet;
    
    private static void Main(string[] args)
    {
        if (args.Length != 0)
        {
            if (args.Contains("-q"))
                Quiet = true;
        }
        
        // Check required Directories + Files
        var result = Paths.CheckRequiredPaths();
        if (!result)
            return;
        
        // Load Config
        var config = Configuration.LoadFromFile(Paths.ConfigPath.FullName);
        _startingUniqueID = config["General"]["StartingUniqueID"].IntValue;
        _maxUniqueID = config["General"]["MaxUniqueID"].IntValue;
        _excludedSongs = config["General"]["ExcludedSongs"].StringValue.Split(',').ToList();
        
        RetrieveSteamData();
        RetrieveSwitchData();
        FilterSwitchFiles();

        UpdateSteamData();
        
        SerializeSteamData();
        EncryptSwitchFiles();

        CleanTempFiles();
        WriteIgnoredSongs();
        
        Console.WriteLine("Finished! :)");
    }

    private static void RetrieveSteamData()
    {
        EncryptionHelper.DecryptFile(Paths.SteamMusicInfo, true);
        EncryptionHelper.DecryptFile(Paths.SteamInitialPossession, true);
        
        var musicInfoJson = Paths.SteamMusicInfoJson;
        var initialPossessionJson = Paths.SteamInitialPossessionJson;

        if (!musicInfoJson.Exists)
        {
            Console.WriteLine("Steam Music Info JSON not found at {0}. Did something go wrong with tns2tool?", musicInfoJson.FullName);
            throw new Exception();
        }

        if (!initialPossessionJson.Exists)
        {
            Console.WriteLine("Steam Initial Possession JSON not found at {0}. Did something go wrong with tns2tool?", initialPossessionJson.FullName);
            throw new Exception();
        }
        
        musicInfoJson.MoveTo(Paths.SteamMusicInfoJsonTemp.FullName, true);
        initialPossessionJson.MoveTo(Paths.SteamInitialPossessionJsonTemp.FullName, true);
        
        _musicInfos = JsonConvert.DeserializeObject<MusicInfo>(File.ReadAllText(musicInfoJson.FullName))!;
        _initialPossessions = JsonConvert.DeserializeObject<InitialPossession>(File.ReadAllText(initialPossessionJson.FullName))!;
    }
    
    private static void RetrieveSwitchData()
    {
        ExtractorHelper.Extract(Paths.SwitchMusicData);
        ExtractorHelper.Extract(Paths.SwitchWordData);
        
        var musicDataJson = Paths.SwitchMusicDataJson;
        var wordDataJson = Paths.SwitchWordDataJson;

        if (!musicDataJson.Exists)
        {
            Console.WriteLine("Switch Music Data JSON not found at {0}. Did something go wrong with DonderfulJSONExtractor?", musicDataJson.FullName);
            throw new Exception();
        }

        if (!wordDataJson.Exists)
        {
            Console.WriteLine("Switch Word Data JSON not found at {0}. Did something go wrong with DonderfulJSONExtractor?", wordDataJson.FullName);
            throw new Exception();
        }
        
        musicDataJson.MoveTo(Paths.TempDirectory.FullName + "/musicdata.json", true);
        wordDataJson.MoveTo(Paths.TempDirectory.FullName + "/worddata.json", true);
        
        _musicData = JsonConvert.DeserializeObject<MusicData>(File.ReadAllText(musicDataJson.FullName))!;
        _wordData = JsonConvert.DeserializeObject<WordData>(File.ReadAllText(wordDataJson.FullName))!;
    }

    private static void FilterSwitchFiles()
    {
        var songDirectory = Paths.SwitchSong;
        var presongDirectory = Paths.SwitchPresong;
        
        var songTempDirectory = Paths.SongTempDirectory;

        foreach (var songFile in songDirectory.GetFiles("*.acb", SearchOption.AllDirectories))
        {
            var fileName = Path.GetFileNameWithoutExtension(songFile.Name);
            if (_excludedSongs.Any(ex => fileName.EndsWith(ex, StringComparison.CurrentCultureIgnoreCase)))
            {
                _ignoredSongs.Add(new Tuple<string, string>(fileName, "Excluded"));
                continue;
            }
            
            songFile.CopyTo(songTempDirectory.FullName + "/" + songFile.Name, true);
            _availableSongs.Add(Path.GetFileNameWithoutExtension(songFile.Name));
        }

        foreach (var presongFile in presongDirectory.GetFiles("*.acb", SearchOption.AllDirectories))
        {
            if (!_availableSongs.Contains(Path.GetFileNameWithoutExtension(presongFile.Name[1..])))
            {
                _ignoredSongs.Add(new Tuple<string, string>(presongFile.Name, "Not in Song Directory"));
                continue;
            }
            presongFile.CopyTo(songTempDirectory.FullName + "/" + presongFile.Name, true);
        }
    }
    
    private static void UpdateSteamData()
    {
        var currentUniqueID = _startingUniqueID;

        for (var index = 0; index < _musicData.MusicDatas.Count; index++)
        {
            var musicData = _musicData.MusicDatas[index];
            var wordData = _wordData.GetSongWordData(musicData.ID);
            var subtitleWordData = _wordData.GetSongSubtitleWordData(musicData.ID);

            if (wordData == null)
            {
                _ignoredSongs.Add(new Tuple<string, string>(musicData.ID, "Word Data Not Found"));
                if (!Quiet)
                    Console.WriteLine("Word Data with ID {0} not found in the Switch Word Data.", musicData.ID);
                continue;
            }

            if (_excludedSongs.Contains(musicData.ID))
                continue;
            if (!_availableSongs.Contains(musicData.SongFileName))
                continue;

            // Already exists in some form
            if (_musicInfos.HasMusicInfo(musicData.ID))
            {
                _ignoredSongs.Add(new Tuple<string, string>(musicData.ID, "Already Exists"));
                if (!Quiet)
                    Console.WriteLine("Music Info with ID {0} already exists in the Steam Music Info.", musicData.ID);
                continue;
            }

            while (_musicInfos.HasID(currentUniqueID))
                currentUniqueID++;

            if (currentUniqueID >= _maxUniqueID)
            {
                // Get all remaining songs
                var remainingSongs = _musicData.MusicDatas.Skip(index).Select(m => m.ID).ToList();
                foreach (var song in remainingSongs)
                    _ignoredSongs.Add(new Tuple<string, string>(song, "Max Unique ID Reached"));

                Console.WriteLine("Reached the maximum Unique ID of {0}.", _maxUniqueID);
                break;
            }

            // Create Music Info
            var musicInfo = new MusicInfoSingle
            {
                UniqueID = currentUniqueID,
                ID = musicData.ID,
                SongFileName = musicData.SongFileName,
                Order = musicData.Order,
                Genre = musicData.Genre,
                BranchEasy = musicData.BranchEasy,
                BranchNormal = musicData.BranchNormal,
                BranchHard = musicData.BranchHard,
                BranchMania = musicData.BranchMania,
                BranchUra = musicData.BranchOni,
                StarEasy = musicData.StarEasy,
                StarNormal = musicData.StarNormal,
                StarHard = musicData.StarHard,
                StarMania = musicData.StarMania,
                StarUra = musicData.StarOni,
                ShinutiEasy = musicData.ShinutiEasy,
                ShinutiNormal = musicData.ShinutiNormal,
                ShinutiHard = musicData.ShinutiHard,
                ShinutiMania = musicData.ShinutiMania,
                ShinutiUra = musicData.ShinutiOni,
                ShinutiEasyDuet = musicData.ShinutiEasyDuet,
                ShinutiNormalDuet = musicData.ShinutiNormalDuet,
                ShinutiHardDuet = musicData.ShinutiHardDuet,
                ShinutiManiaDuet = musicData.ShinutiManiaDuet,
                ShinutiUraDuet = musicData.ShinutiOniDuet,
                SongNameJP = wordData.JapaneseText,
                SongSubJP = subtitleWordData?.JapaneseText ?? string.Empty,
                SongNameEN = wordData.EnglishText,
                SongSubEN = subtitleWordData?.EnglishText ?? string.Empty,
                SongNameFR = wordData.FrenchText,
                SongSubFR = subtitleWordData?.FrenchText ?? string.Empty,
                SongNameIT = wordData.ItalianText,
                SongSubIT = subtitleWordData?.ItalianText ?? string.Empty,
                SongNameDE = wordData.GermanText,
                SongSubDE = subtitleWordData?.GermanText ?? string.Empty,
                SongNameES = wordData.SpanishText,
                SongSubES = subtitleWordData?.SpanishText ?? string.Empty,
                SongNameTW = wordData.TaiwaneseText,
                SongSubTW = subtitleWordData?.TaiwaneseText ?? string.Empty,
                SongNameCN = wordData.ChineseSimplifiedText,
                SongSubCN = subtitleWordData?.ChineseSimplifiedText ?? string.Empty,
                SongNameKO = wordData.KoreanText,
                SongSubKO = subtitleWordData?.KoreanText ?? string.Empty
            };

            var possessionInfo = new InitialPossessionSingle()
            {
                Type = 4,
                ID = currentUniqueID
            };

            _musicInfos.MusicInfos.Add(musicInfo);
            _initialPossessions.InitialPossessions.Add(possessionInfo);
        }
    }

    private static void SerializeSteamData()
    {
        var serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            StringEscapeHandling = StringEscapeHandling.Default
        };
        
        var musicInfoJson = JsonConvert.SerializeObject(_musicInfos, serializerSettings);
        var initialPossessionJson = JsonConvert.SerializeObject(_initialPossessions, serializerSettings);
        
        File.WriteAllText(Paths.SteamMusicInfoJsonTemp.FullName, musicInfoJson);
        File.WriteAllText(Paths.SteamInitialPossessionJsonTemp.FullName, initialPossessionJson);
        
        EncryptionHelper.EncryptFile(Paths.SteamMusicInfoJsonTemp, true);
        EncryptionHelper.EncryptFile(Paths.SteamInitialPossessionJsonTemp, true);
        
        Paths.SteamMusicInfoFinalTemp.MoveTo(Paths.SteamMusicInfoOut.FullName, true);
        Paths.SteamInitialPossessionFinalTemp.MoveTo(Paths.SteamInitialPossessionOut.FullName, true);
    }
    
    private static void EncryptSwitchFiles()
    {
        var csvDirectory = Paths.SwitchCsv;
        var fumenDirectory = Paths.SwitchFumen;
        var fumenTempDirectory = Paths.FumenTempDirectory;
        var songTempDirectory = Paths.SongTempDirectory;
        
        // Move fumen files to temp directory as they're in subfolders in the Switch Files
        Console.WriteLine("Copying Fumen Files to Temp Directory...");
        foreach (var fumenFile in fumenDirectory.GetFiles("*.bin", SearchOption.AllDirectories))
            fumenFile.CopyTo(fumenTempDirectory.FullName + "/" + fumenFile.Name, true);
        
        EncryptionHelper.EncryptPath(csvDirectory, true);
        EncryptionHelper.EncryptPath(fumenTempDirectory, true);
        EncryptionHelper.EncryptPath(songTempDirectory, false);
        
        Console.WriteLine("Moving Encrypted CSV Files to Steam Directories...");
        var csvOutDirectory = new DirectoryInfo(Paths.SwitchCsv + "_encrypted");
        foreach (var outCsv in csvOutDirectory.EnumerateFiles())
            outCsv.MoveTo(Paths.SteamCsvOut.FullName + "/" + outCsv.Name, true);
        
        Console.WriteLine("Moving Encrypted Fumen Files to Steam Directories...");
        var fumenOutDirectory = new DirectoryInfo(Paths.FumenTempDirectory + "_encrypted");
        foreach (var outFumen in fumenOutDirectory.EnumerateFiles())
            outFumen.MoveTo(Paths.SteamFumenOut.FullName + "/" + outFumen.Name, true);
        
        Console.WriteLine("Moving Encrypted Song Files to Steam Directories...");
        var songOutDirectory = new DirectoryInfo(Paths.SongTempDirectory + "_encrypted");
        foreach (var outSong in songOutDirectory.EnumerateFiles())
            outSong.MoveTo(Paths.SteamSongOut.FullName + "/" + outSong.Name, true);
    }
    
    private static void CleanTempFiles()
    {
        Paths.FumenTempDirectory.Delete(true);
        Paths.SongTempDirectory.Delete(true);
    }
    
    private static void WriteIgnoredSongs()
    {
        var ignoredSongs = Paths.IgnoredSongs;
        if (ignoredSongs.Exists)
            ignoredSongs.Delete();
        
        var sb = new StringBuilder();
        sb.AppendLine($"Ignored Count: {_ignoredSongs.Count}");
        sb.AppendLine("");
        
        foreach (var (song, reason) in _ignoredSongs)
            sb.AppendLine($"{song} - {reason}");
        
        File.WriteAllText(ignoredSongs.FullName, sb.ToString());
    }
}