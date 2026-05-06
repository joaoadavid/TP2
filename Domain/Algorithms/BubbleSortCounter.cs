namespace TP2.Domain.Algorithms;

public sealed class BubbleSortCounter : ISortingOperationCounter
{
    public string Name => "BubbleSort";

    public long CountOperations(IReadOnlyList<int> values)
    {
        int n = values.Count;
        if (n <= 1)
        {
            return 0;
        }

        long comparisons = (long)n * (n - 1) / 2;
        long swaps = CountInversions(values);
        return comparisons + (3L * swaps);
    }

    private static long CountInversions(IReadOnlyList<int> values)
    {
        int[] arr = values.ToArray();
        int[] buffer = new int[arr.Length];
        return MergeCount(arr, buffer, 0, arr.Length - 1);
    }

    private static long MergeCount(int[] arr, int[] buffer, int left, int right)
    {
        if (left >= right)
        {
            return 0;
        }

        int mid = left + ((right - left) / 2);
        long inversions = 0;

        inversions += MergeCount(arr, buffer, left, mid);
        inversions += MergeCount(arr, buffer, mid + 1, right);

        int i = left;
        int j = mid + 1;
        int k = left;

        while (i <= mid && j <= right)
        {
            if (arr[i] <= arr[j])
            {
                buffer[k++] = arr[i++];
            }
            else
            {
                buffer[k++] = arr[j++];
                inversions += (mid - i + 1);
            }
        }

        while (i <= mid)
        {
            buffer[k++] = arr[i++];
        }

        while (j <= right)
        {
            buffer[k++] = arr[j++];
        }

        for (int p = left; p <= right; p++)
        {
            arr[p] = buffer[p];
        }

        return inversions;
    }
}
