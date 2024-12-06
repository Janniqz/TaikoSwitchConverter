using System.Diagnostics;
using System.Reflection;

namespace TaikoSwitchConverter.Helpers;

public static class EncryptionHelper
{
    public static void EncryptFile(FileInfo filePath, bool gzip) => Process.Start(GetBaseProcessInfo(true, false, $"\"{filePath.FullName}\"", gzip))!.WaitForExit();

    public static void EncryptPath(DirectoryInfo directoryPath, bool gzip) => Process.Start(GetBaseProcessInfo(true, true, $"\"{directoryPath.FullName}\"", gzip))!.WaitForExit();

    public static void DecryptFile(FileInfo filePath, bool gzip) => Process.Start(GetBaseProcessInfo(false, false, $"\"{filePath.FullName}\"", gzip))!.WaitForExit();

    public static void DecryptPath(DirectoryInfo directoryPath, bool gzip) => Process.Start(GetBaseProcessInfo(false, true, $"\"{directoryPath.FullName}\"", gzip))!.WaitForExit();

    private static ProcessStartInfo GetBaseProcessInfo(bool encrypt, bool directory, string path, bool gzip)
    {
        var arguments = string.Empty;
        if (encrypt)
            arguments += "-e";
        else
            arguments += "-d";
        
        if (directory)
            arguments += $" -inPath {path}";
        else
            arguments += $" -inFile {path}";
        
        if (gzip)
            arguments += " -gzip";
        
        return new ProcessStartInfo
        {
            FileName = Paths.TnsToolPath.FullName,
            UseShellExecute = false,
            CreateNoWindow = false,
            Arguments = arguments
        };
    }
}