using System.Globalization;
using TP2.Application.Contracts;

namespace TP2.Infrastructure.FileSystem;

public sealed class NumberFileReader : INumberFileReader
{
    public IReadOnlyList<int> ReadNumbers(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Arquivo nao encontrado: {filePath}");
        }

        var numbers = new List<int>();

        foreach (string raw in File.ReadLines(filePath))
        {
            string line = raw.Trim();
            if (line.Length == 0)
            {
                break;
            }

            numbers.Add(int.Parse(line, CultureInfo.InvariantCulture));
        }

        return numbers;
    }
}
