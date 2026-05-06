namespace TP2.Domain.Algorithms;

public interface ISortingOperationCounter
{
    string Name { get; }

    long CountOperations(IReadOnlyList<int> values);
}
