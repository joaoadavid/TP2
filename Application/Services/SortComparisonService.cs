using TP2.Application.Contracts;
using TP2.Application.Models;
using TP2.Domain.Algorithms;

namespace TP2.Application.Services;

public sealed class SortComparisonService
{
    private readonly INumberFileReader _numberFileReader;
    private readonly IHtmlReportBuilder _htmlReportBuilder;
    private readonly ITextFileWriter _textFileWriter;
    private readonly Dictionary<string, ISortingOperationCounter> _counters;

    public SortComparisonService(
        INumberFileReader numberFileReader,
        IHtmlReportBuilder htmlReportBuilder,
        ITextFileWriter textFileWriter,
        IEnumerable<ISortingOperationCounter> counters)
    {
        _numberFileReader = numberFileReader;
        _htmlReportBuilder = htmlReportBuilder;
        _textFileWriter = textFileWriter;
        _counters = counters.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);
    }

    public void Execute(CompareOptions options)
    {
        if (options.InputFiles.Count == 0)
        {
            throw new ArgumentException("Informe ao menos um arquivo de entrada.");
        }

        var rows = new List<ComparisonRow>();

        foreach (string file in options.InputFiles)
        {
            IReadOnlyList<int> values = _numberFileReader.ReadNumbers(file);

            string bubbleCounterName = options.BubbleSortMode == BubbleSortMode.Literal
                ? "BubbleSortLiteral"
                : "BubbleSort";

            long bubble = Counter(bubbleCounterName).CountOperations(values);
            long quick = Counter("QuickSort").CountOperations(values);
            long bucket = Counter("BucketSort").CountOperations(values);

            rows.Add(new ComparisonRow(file, bubble, quick, bucket));
        }

        string html = _htmlReportBuilder.Build(rows);
        _textFileWriter.WriteAllText(options.OutputPath, html);
    }

    private ISortingOperationCounter Counter(string name)
    {
        if (_counters.TryGetValue(name, out ISortingOperationCounter? counter))
        {
            return counter;
        }

        throw new InvalidOperationException($"Contador de algoritmo nao registrado: {name}");
    }
}
