using TP2.Composition;
using TP2.Presentation.Cli;

internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            ParsedCommand command = CliArgumentParser.Parse(args);

            switch (command.Kind)
            {
                case CommandKind.ShowUsage:
                    UsagePrinter.Print();
                    return 1;

                case CommandKind.GenerateTests:
                    return ExecuteGenerateTests(command);

                case CommandKind.Compare:
                    return ExecuteComparison(command);

                default:
                    throw new InvalidOperationException("Comando invalido.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro: {ex.Message}");
            return 1;
        }
    }

    private static int ExecuteGenerateTests(ParsedCommand command)
    {
        if (command.GenerateTestsOptions is null)
        {
            throw new InvalidOperationException("Opcoes de geracao nao encontradas.");
        }

        var service = AppCompositionRoot.BuildTestDataGenerationService();
        service.Execute(command.GenerateTestsOptions);

        Console.WriteLine($"Arquivos gerados em: {Path.GetFullPath(command.GenerateTestsOptions.OutDir)}");
        return 0;
    }

    private static int ExecuteComparison(ParsedCommand command)
    {
        if (command.CompareOptions is null)
        {
            throw new InvalidOperationException("Opcoes de comparacao nao encontradas.");
        }

        var service = AppCompositionRoot.BuildSortComparisonService();

        service.Execute(command.CompareOptions);

        Console.WriteLine($"HTML gerado com sucesso em: {Path.GetFullPath(command.CompareOptions.OutputPath)}");
        return 0;
    }
}
