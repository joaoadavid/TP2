using System.Globalization;
using TP2.Application.Models;

namespace TP2.Application.Services;

public sealed class TestDataGenerationService
{
    public void Execute(GenerateTestsOptions options)
    {
        Directory.CreateDirectory(options.OutDir);

        foreach (int size in options.Sizes)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Todos os tamanhos devem ser positivos.");
            }

            var rng = new Random(options.Seed + size);

            var random = new int[size];
            for (int i = 0; i < size; i++)
            {
                random[i] = rng.Next(0, size * 10 + 1);
            }

            int[] ascending = Enumerable.Range(0, size).ToArray();
            int[] descending = Enumerable.Range(0, size).Select(v => size - 1 - v).ToArray();

            WriteCase(Path.Combine(options.OutDir, $"random_{size}.txt"), random);
            WriteCase(Path.Combine(options.OutDir, $"ascending_{size}.txt"), ascending);
            WriteCase(Path.Combine(options.OutDir, $"descending_{size}.txt"), descending);

            WriteCase(Path.Combine(options.OutDir, $"randomico_{size}.txt"), random);
            WriteCase(Path.Combine(options.OutDir, $"ordenado_{size}.txt"), ascending);
            WriteCase(Path.Combine(options.OutDir, $"decrescente_{size}.txt"), descending);
        }
    }

    private static void WriteCase(string path, IEnumerable<int> values)
    {
        using var writer = new StreamWriter(path, false);
        foreach (int value in values)
        {
            writer.WriteLine(value.ToString(CultureInfo.InvariantCulture));
        }

        writer.WriteLine();
    }
}
