using System.Diagnostics;
using System.Reflection;

namespace TaikoSwitchConverter.Helpers;

public static class ExtractorHelper
{
    public static void Extract(FileInfo filePath) => Process.Start(GetBaseProcessInfo(filePath))!.WaitForExit();

    private static ProcessStartInfo GetBaseProcessInfo(FileInfo path)
    {
        return new ProcessStartInfo
        {
            FileName = Paths.DonderfulExtractorPath.FullName,
            WorkingDirectory = path.Directory!.FullName,
            UseShellExecute = false,
            CreateNoWindow = false,
            Arguments = path.FullName
        };
    }
}