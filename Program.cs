using Newtonsoft.Json;
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
    
    private const int StartingUniqueID = 1500;
    private const int MaxUniqueID = 3000;

    private static void Main()
    {
        var result = Paths.CheckRequiredPaths();
        if (!result)
            return;
        
        RetrieveSteamData();
        RetrieveSwitchData();

        UpdateSteamData();
        
        SerializeSteamData();
        EncryptSwitchFiles();
    }

    private static void RetrieveSteamData()
    {
        EncryptionHelper.DecryptFile(Paths.SteamMusicInfo, true);
        EncryptionHelper.DecryptFile(Paths.SteamInitialPossession, true);
        
        var musicInfoJson = Paths.SteamMusicInfoJson;
        var initialPossessionJson = Paths.SteamInitialPossessionJson;
        
        if (!musicInfoJson.Exists)
            return;
        if (!initialPossessionJson.Exists)
            return;
        
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
            return;
        if (!wordDataJson.Exists)
            return;
        
        musicDataJson.MoveTo(Paths.TempDirectory.FullName + "/musicdata.json", true);
        wordDataJson.MoveTo(Paths.TempDirectory.FullName + "/worddata.json", true);
        
        _musicData = JsonConvert.DeserializeObject<MusicData>(File.ReadAllText(musicDataJson.FullName))!;
        _wordData = JsonConvert.DeserializeObject<WordData>(File.ReadAllText(wordDataJson.FullName))!;
    }

    private static void UpdateSteamData()
    {
        var currentUniqueID = StartingUniqueID;
        
        foreach (var musicData in _musicData.MusicDatas)
        {
            var wordData = _wordData.GetSongWordData(musicData.ID);
            
            // Already exists in some form
            if (_musicInfos.HasMusicInfo(musicData.ID))
                continue;

            while (_musicInfos.HasID(currentUniqueID))
                currentUniqueID++;
            
            if (currentUniqueID >= MaxUniqueID)
                break;
            
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
                SongNameEN = wordData.EnglishText,
                SongNameFR = wordData.FrenchText,
                SongNameIT = wordData.ItalianText,
                SongNameDE = wordData.GermanText,
                SongNameES = wordData.SpanishText,
                SongNameTW = wordData.TaiwaneseText,
                SongNameCN = wordData.ChineseSimplifiedText,
                SongNameKO = wordData.KoreanText
            };

            var valid = musicInfo.UpdateSongName();
            if (!valid)
                continue;
            
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
        var songDirectory = Paths.SwitchSong;
        var presongDirectory = Paths.SwitchPresong;
        
        // Move fumen files to temp directory as they're in subfolders in the Switch Files
        foreach (var fumenFile in fumenDirectory.GetFiles("*.bin", SearchOption.AllDirectories))
            fumenFile.CopyTo(fumenTempDirectory.FullName + "/" + fumenFile.Name, true);
        
        EncryptionHelper.EncryptPath(csvDirectory, true);
        EncryptionHelper.EncryptPath(fumenTempDirectory, true);
        EncryptionHelper.EncryptPath(songDirectory, false);
        EncryptionHelper.EncryptPath(presongDirectory, false);
        
        var csvOutDirectory = new DirectoryInfo(Paths.SwitchCsv + "_encrypted");
        foreach (var outCsv in csvOutDirectory.EnumerateFiles())
            outCsv.MoveTo(Paths.SteamCsvOut.FullName + "/" + outCsv.Name, true);
        
        var fumenOutDirectory = new DirectoryInfo(Paths.FumenTempDirectory + "_encrypted");
        foreach (var outFumen in fumenOutDirectory.EnumerateFiles())
            outFumen.MoveTo(Paths.SteamFumenOut.FullName + "/" + outFumen.Name, true);
        
        var songOutDirectory = new DirectoryInfo(Paths.SwitchSong + "_encrypted");
        foreach (var outSong in songOutDirectory.EnumerateFiles())
            outSong.MoveTo(Paths.SteamSongOut.FullName + "/" + outSong.Name, true);
        
        var presongOutDirectory = new DirectoryInfo(Paths.SwitchPresong + "_encrypted");
        foreach (var outPresong in presongOutDirectory.EnumerateFiles())
            outPresong.MoveTo(Paths.SteamSongOut.FullName + "/" + outPresong.Name, true);
    }
}