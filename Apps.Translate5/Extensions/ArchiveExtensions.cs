using System.IO.Compression;

namespace Apps.Translate5.Extensions;

public static class ArchiveExtensions
{
    public static async Task AddFileToZip(this ZipArchive archive, string path, byte[] file)
    {
        var entry = archive.CreateEntry(path);
        await using var entryStream = entry.Open();

        await entryStream.WriteAsync(file, 0, file.Length);
    }
}