namespace TP2.Domain.Algorithms;

public sealed class BubbleSortLiteralCounter : ISortingOperationCounter
{
    public string Name => "BubbleSortLiteral";

    public long CountOperations(IReadOnlyList<int> values)
    {
        int[] arr = values.ToArray();
        int n = arr.Length;
        long ops = 0;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                ops++; // comparacao arr[j] > arr[j + 1]
                if (arr[j] > arr[j + 1])
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    ops += 3; // trocas/escritas
                }
            }
        }

        return ops;
    }
}
