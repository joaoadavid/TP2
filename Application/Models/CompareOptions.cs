namespace TP2.Application.Models;

public sealed class CompareOptions
{
    public CompareOptions(IReadOnlyList<string> inputFiles, string outputPath, BubbleSortMode bubbleSortMode)
    {
        InputFiles = inputFiles;
        OutputPath = outputPath;
        BubbleSortMode = bubbleSortMode;
    }

    public IReadOnlyList<string> InputFiles { get; }

    public string OutputPath { get; }

    public BubbleSortMode BubbleSortMode { get; }
}
