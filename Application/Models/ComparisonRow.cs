namespace TP2.Application.Models;

public sealed class ComparisonRow
{
    public ComparisonRow(string fileName, long bubbleSortOps, long quickSortOps, long bucketSortOps)
    {
        FileName = fileName;
        BubbleSortOps = bubbleSortOps;
        QuickSortOps = quickSortOps;
        BucketSortOps = bucketSortOps;
    }

    public string FileName { get; }

    public long BubbleSortOps { get; }

    public long QuickSortOps { get; }

    public long BucketSortOps { get; }
}
