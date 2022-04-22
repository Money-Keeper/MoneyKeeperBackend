﻿namespace MoneyKeeper.Domain.Providers.FilesProvider;

public sealed class PdfProvider : FilesProviderBase
{
    public PdfProvider(string rootDirectoryPath) : base(rootDirectoryPath)
    {
    }

    public override FileType FileType { get; } = FileType.Pdf;

    public override async Task<byte[]> GetAsync(string relativePath)
    {
        string absolutePath = Path.Combine(_rootDirectoryPath, relativePath);

        using Stream fileStream = File.OpenRead(absolutePath);

        byte[] file = new byte[fileStream.Length];

        await fileStream.ReadAsync(file);

        return file;
    }

    public override async Task<string> SaveAsync(ReadOnlyMemory<byte> file)
    {
        string fileName = Guid.NewGuid().ToString() + FileExtensions.Pdf;
        string fileRelativePath = GetFileRelativePath(fileName);
        string fileAbsolutePath = Path.Combine(_rootDirectoryPath, fileRelativePath);

        using Stream fileStream = File.Create(fileAbsolutePath);

        await fileStream.WriteAsync(file);

        return fileRelativePath;
    }
}