using System.Diagnostics;
using System.Reflection;

namespace TaikoSwitchConverter.Helpers;

public static class EncryptionHelper
{
    public static void EncryptFile(FileInfo filePath, bool gzip)
    {
        Console.WriteLine("Encrypting {0} (gzip: {1})...", filePath.FullName, gzip);
        Process.Start(GetBaseProcessInfo(true, false, $"\"{filePath.FullName}\"", gzip))!.WaitForExit();
    }

    public static void EncryptPath(DirectoryInfo directoryPath, bool gzip)
    {
        Console.WriteLine("Encrypting Files in {0} (gzip: {1})...", directoryPath.FullName, gzip);
        Process.Start(GetBaseProcessInfo(true, true, $"\"{directoryPath.FullName}\"", gzip))!.WaitForExit();
    }

    public static void DecryptFile(FileInfo filePath, bool gzip)
    {
        Console.WriteLine("Decrypting {0} (gzip: {1})...", filePath.FullName, gzip);
        Process.Start(GetBaseProcessInfo(false, false, $"\"{filePath.FullName}\"", gzip))!.WaitForExit();
    }

    public static void DecryptPath(DirectoryInfo directoryPath, bool gzip)
    {
        Console.WriteLine("Decrypting Files in {0} (gzip: {1})...", directoryPath.FullName, gzip);
        Process.Start(GetBaseProcessInfo(false, true, $"\"{directoryPath.FullName}\"", gzip))!.WaitForExit();
    }

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
            UseShellExecute = true,
            CreateNoWindow = false,
            Arguments = arguments
        };
    }
}