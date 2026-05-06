namespace TP2.Application.Contracts;

public interface INumberFileReader
{
    IReadOnlyList<int> ReadNumbers(string filePath);
}
