namespace TP2.Presentation.Cli;

public static class UsagePrinter
{
    public static void Print()
    {
        Console.WriteLine("Uso:");
        Console.WriteLine("  dotnet run -- [arquivo1] [arquivo2] ... -o resultado.html [--bubble-mode optimized|literal]");
        Console.WriteLine();
        Console.WriteLine("Gerar casos de teste:");
        Console.WriteLine("  dotnet run -- --generate-tests [--outdir test_data] [--sizes 100,1000,10000,100000] [--seed 42]");
    }
}
