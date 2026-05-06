namespace TP2.Application.Contracts;

public interface ITextFileWriter
{
    void WriteAllText(string path, string content);
}
