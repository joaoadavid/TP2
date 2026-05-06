namespace TP2.Domain.Algorithms;

public sealed class QuickSortCounter : ISortingOperationCounter
{
    public string Name => "QuickSort";

    public long CountOperations(IReadOnlyList<int> values)
    {
        int[] arr = values.ToArray();
        long ops = 0;

        if (arr.Length <= 1)
        {
            return 0;
        }

        static void Swap(int[] array, int a, int b)
        {
            if (a == b)
            {
                return;
            }

            (array[a], array[b]) = (array[b], array[a]);
        }

        int MedianOfThree(int low, int high)
        {
            int mid = low + ((high - low) / 2);

            ops++;
            if (arr[low] > arr[mid])
            {
                Swap(arr, low, mid);
                ops += 3;
            }

            ops++;
            if (arr[low] > arr[high])
            {
                Swap(arr, low, high);
                ops += 3;
            }

            ops++;
            if (arr[mid] > arr[high])
            {
                Swap(arr, mid, high);
                ops += 3;
            }

            return mid;
        }

        int Partition(int low, int high)
        {
            int pivotIndex = MedianOfThree(low, high);
            if (pivotIndex != high)
            {
                Swap(arr, pivotIndex, high);
                ops += 3;
            }

            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                ops++;
                if (arr[j] <= pivot)
                {
                    i++;
                    if (i != j)
                    {
                        Swap(arr, i, j);
                        ops += 3;
                    }
                }
            }

            if (i + 1 != high)
            {
                Swap(arr, i + 1, high);
                ops += 3;
            }

            return i + 1;
        }

        var stack = new Stack<(int Low, int High)>();
        stack.Push((0, arr.Length - 1));

        while (stack.Count > 0)
        {
            var (low, high) = stack.Pop();
            if (low >= high)
            {
                continue;
            }

            int pivot = Partition(low, high);

            int leftLow = low;
            int leftHigh = pivot - 1;
            int rightLow = pivot + 1;
            int rightHigh = high;

            int leftSize = leftHigh - leftLow + 1;
            int rightSize = rightHigh - rightLow + 1;

            if (leftSize > rightSize)
            {
                if (leftLow < leftHigh)
                {
                    stack.Push((leftLow, leftHigh));
                }

                if (rightLow < rightHigh)
                {
                    stack.Push((rightLow, rightHigh));
                }
            }
            else
            {
                if (rightLow < rightHigh)
                {
                    stack.Push((rightLow, rightHigh));
                }

                if (leftLow < leftHigh)
                {
                    stack.Push((leftLow, leftHigh));
                }
            }
        }

        return ops;
    }
}
