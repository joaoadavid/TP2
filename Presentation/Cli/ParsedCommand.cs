using TP2.Application.Models;

namespace TP2.Presentation.Cli;

public enum CommandKind
{
    ShowUsage,
    Compare,
    GenerateTests
}

public sealed class ParsedCommand
{
    public ParsedCommand(CommandKind kind, CompareOptions? compareOptions = null, GenerateTestsOptions? generateTestsOptions = null)
    {
        Kind = kind;
        CompareOptions = compareOptions;
        GenerateTestsOptions = generateTestsOptions;
    }

    public CommandKind Kind { get; }

    public CompareOptions? CompareOptions { get; }

    public GenerateTestsOptions? GenerateTestsOptions { get; }
}
