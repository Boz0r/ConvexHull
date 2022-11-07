using Xunit.Sdk;

namespace ConvexHull.Test;

public class RepeatAttribute : DataAttribute
{
    private readonly int _count;

    public RepeatAttribute(int count)
    {
        if (count < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(count),
                "Repeat count must be greater than 0.");
        }

        _count = count;
    }

    public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
    {
        foreach (var iterationNumber in Enumerable.Range(start: 1, count: _count))
        {
            RandomProvider.Random = new Random(iterationNumber);
            yield return Array.Empty<object>();
        }
    }
}
