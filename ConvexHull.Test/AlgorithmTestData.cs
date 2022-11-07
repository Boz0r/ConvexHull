using System.Collections;
using System.Numerics;

namespace ConvexHull.Test;

public class AlgorithmTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<Vector3>
            {
                new(3, 0, 0),
                new(1, 1, 0),
                new(0, 0, 0),
                new(2, 0, 0),
                new(1, 0, 0),
                new(2, 1, 0),
                new(1, -1, 0),
                new(2, -1, 0),
            },
            new List<Vector3>
            {
                new(0, 0, 0),
                new(1, 1, 0),
                new(2, 1, 0),
                new(3, 0, 0),
                new(2, -1, 0),
                new(1, -1, 0),
            },
        };
        yield return new object[]
        {
            new List<Vector3>
            {
                new(1, 1, 0),
                new(-1, -1, 0),
                new(1, -1, 0),
                new(-1, 1, 0),
            },
            new List<Vector3>
            {
                new(1, -1, 0),
                new(1, 1, 0),
                new(-1, 1, 0),
                new(-1, -1, 0),
            },
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
