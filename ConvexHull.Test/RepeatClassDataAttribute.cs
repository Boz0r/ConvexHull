namespace ConvexHull.Test;

public sealed class RepeatClassDataAttribute : ClassDataAttribute
{
    private readonly int count;

    public RepeatClassDataAttribute(int count, Type type) : base(type)
    {
        if (count < 1)
        {
            throw new System.ArgumentOutOfRangeException(
                paramName: nameof(count),
                message: "Repeat count must be greater than 0."
            );
        }

        this.count = count;
    }

    public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
    {
        foreach (var iterationNumber in Enumerable.Range(start: 1, count: this.count))
        {
            foreach (var data in base.GetData(testMethod).ToList())
            {
                List<object> objects = new() { iterationNumber };
                objects.AddRange(data);
                yield return objects.ToArray();
            }
        }
    }
}
