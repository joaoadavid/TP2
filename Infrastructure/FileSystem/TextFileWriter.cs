using TP2.Application.Contracts;

namespace TP2.Infrastructure.FileSystem;

public sealed class TextFileWriter : ITextFileWriter
{
    public void WriteAllText(string path, string content)
    {
        File.WriteAllText(path, content);
    }
}
