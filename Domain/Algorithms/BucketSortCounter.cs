namespace TP2.Domain.Algorithms;

public sealed class BucketSortCounter : ISortingOperationCounter
{
    public string Name => "BucketSort";

    public long CountOperations(IReadOnlyList<int> values)
    {
        int[] arr = values.ToArray();
        int n = arr.Length;

        if (n <= 1)
        {
            return 0;
        }

        long ops = 0;

        int min = arr[0];
        int max = arr[0];
        for (int i = 1; i < n; i++)
        {
            ops += 2;
            if (arr[i] < min)
            {
                min = arr[i];
            }

            if (arr[i] > max)
            {
                max = arr[i];
            }
        }

        if (min == max)
        {
            return ops;
        }

        int bucketCount = n;
        var buckets = new List<int>[bucketCount];
        for (int i = 0; i < bucketCount; i++)
        {
            buckets[i] = new List<int>();
        }

        long range = (long)max - min + 1;

        foreach (int value in arr)
        {
            int index = (int)(((long)(value - min) * bucketCount) / range);
            if (index == bucketCount)
            {
                index--;
            }

            buckets[index].Add(value);
            ops += 2;
        }

        foreach (List<int> bucket in buckets)
        {
            for (int i = 1; i < bucket.Count; i++)
            {
                int key = bucket[i];
                int j = i - 1;

                while (j >= 0)
                {
                    ops++;
                    if (bucket[j] > key)
                    {
                        bucket[j + 1] = bucket[j];
                        ops++;
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }

                bucket[j + 1] = key;
                ops++;
            }

            ops += bucket.Count;
        }

        return ops;
    }
}
