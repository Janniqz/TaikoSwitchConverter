using System.Diagnostics;

namespace TaikoSwitchConverter.Helpers;

public static class ExtractorHelper
{
    public static void Extract(FileInfo filePath)
    {
        Console.WriteLine("Extracting {0}...", filePath.FullName);
        Process.Start(GetBaseProcessInfo(filePath))!.WaitForExit();
    }

    private static ProcessStartInfo GetBaseProcessInfo(FileInfo path)
    {
        return new ProcessStartInfo
        {
            FileName = Paths.DonderfulExtractorPath.FullName,
            WorkingDirectory = path.Directory!.FullName,
            UseShellExecute = true,
            CreateNoWindow = false,
            Arguments = path.FullName
        };
    }
}