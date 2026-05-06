using TP2.Application.Services;
using TP2.Domain.Algorithms;
using TP2.Infrastructure.FileSystem;
using TP2.Presentation.Reporting;

namespace TP2.Composition;

public static class AppCompositionRoot
{
    public static SortComparisonService BuildSortComparisonService()
    {
        return new SortComparisonService(
            new NumberFileReader(),
            new HtmlReportBuilder(),
            new TextFileWriter(),
            new ISortingOperationCounter[]
            {
                new BubbleSortCounter(),
                new BubbleSortLiteralCounter(),
                new QuickSortCounter(),
                new BucketSortCounter()
            });
    }

    public static TestDataGenerationService BuildTestDataGenerationService()
    {
        return new TestDataGenerationService();
    }
}
