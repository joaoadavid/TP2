using System.Globalization;
using TP2.Application.Models;

namespace TP2.Presentation.Cli;

public static class CliArgumentParser
{
    public static ParsedCommand Parse(string[] args)
    {
        if (args.Length == 0)
        {
            return new ParsedCommand(CommandKind.ShowUsage);
        }

        if (args[0].Equals("--generate-tests", StringComparison.OrdinalIgnoreCase))
        {
            return ParseGenerateTests(args.Skip(1).ToArray());
        }

        return ParseCompare(args);
    }

    private static ParsedCommand ParseCompare(string[] args)
    {
        string outputPath = "resultado.html";
        BubbleSortMode bubbleSortMode = BubbleSortMode.Optimized;
        var rawInputFiles = new List<string>();

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            if (arg is "-o" or "--output")
            {
                if (i + 1 >= args.Length)
                {
                    throw new ArgumentException("Informe um caminho apos -o/--output.");
                }

                outputPath = args[++i];
            }
            else if (arg.Equals("--bubble-mode", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 >= args.Length)
                {
                    throw new ArgumentException("Informe um valor apos --bubble-mode: optimized ou literal.");
                }

                bubbleSortMode = ParseBubbleSortMode(args[++i]);
            }
            else
            {
                rawInputFiles.Add(arg);
            }
        }

        var inputFiles = ExpandInputFiles(rawInputFiles);

        return new ParsedCommand(
            CommandKind.Compare,
            new TP2.Application.Models.CompareOptions(inputFiles, outputPath, bubbleSortMode));
    }

    private static BubbleSortMode ParseBubbleSortMode(string value)
    {
        if (value.Equals("optimized", StringComparison.OrdinalIgnoreCase))
        {
            return BubbleSortMode.Optimized;
        }

        if (value.Equals("literal", StringComparison.OrdinalIgnoreCase))
        {
            return BubbleSortMode.Literal;
        }

        throw new ArgumentException("Valor invalido para --bubble-mode. Use: optimized ou literal.");
    }

    private static List<string> ExpandInputFiles(IEnumerable<string> rawInputFiles)
    {
        var expanded = new List<string>();

        foreach (string input in rawInputFiles)
        {
            if (!HasWildcard(input))
            {
                expanded.Add(input);
                continue;
            }

            string directory = Path.GetDirectoryName(input) ?? ".";
            string pattern = Path.GetFileName(input);

            if (pattern.Length == 0)
            {
                pattern = "*";
            }

            if (!Directory.Exists(directory))
            {
                throw new ArgumentException($"Diretorio nao encontrado para o padrao: {input}");
            }

            string[] matches = Directory.GetFiles(directory, pattern, SearchOption.TopDirectoryOnly);

            if (matches.Length == 0)
            {
                throw new ArgumentException($"Nenhum arquivo encontrado para o padrao: {input}");
            }

            Array.Sort(matches, StringComparer.OrdinalIgnoreCase);
            expanded.AddRange(matches);
        }

        return expanded.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    private static bool HasWildcard(string value)
    {
        return value.IndexOfAny(new[] { '*', '?' }) >= 0;
    }

    private static ParsedCommand ParseGenerateTests(string[] args)
    {
        string outDir = "test_data";
        int seed = 42;
        var sizes = new List<int> { 100, 1000, 10_000, 100_000 };

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            if (arg.Equals("--outdir", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 >= args.Length)
                {
                    throw new ArgumentException("Informe um caminho apos --outdir.");
                }

                outDir = args[++i];
            }
            else if (arg.Equals("--seed", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 >= args.Length)
                {
                    throw new ArgumentException("Informe um numero apos --seed.");
                }

                seed = int.Parse(args[++i], CultureInfo.InvariantCulture);
            }
            else if (arg.Equals("--sizes", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 >= args.Length)
                {
                    throw new ArgumentException("Informe valores apos --sizes. Exemplo: 100,1000,10000");
                }

                sizes = args[++i]
                    .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s, CultureInfo.InvariantCulture))
                    .ToList();
            }
            else
            {
                throw new ArgumentException($"Parametro desconhecido para geracao: {arg}");
            }
        }

        return new ParsedCommand(
            CommandKind.GenerateTests,
            generateTestsOptions: new GenerateTestsOptions(outDir, seed, sizes));
    }
}
