namespace TP2.Application.Models;

public sealed class GenerateTestsOptions
{
    public GenerateTestsOptions(string outDir, int seed, IReadOnlyList<int> sizes)
    {
        OutDir = outDir;
        Seed = seed;
        Sizes = sizes;
    }

    public string OutDir { get; }

    public int Seed { get; }

    public IReadOnlyList<int> Sizes { get; }
}
