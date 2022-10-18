using System.Numerics;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using ConvexHull.Algorithms;

namespace ConvexHull.Benchmark;
public class KSBridgeBenchmarks
{
    [Benchmark, Arguments(1), Arguments(10), Arguments(100), Arguments(1000)]
    public (Vector3 i, Vector3 j) DefaultBridgeStrategy(int count)
    {
        var input = new Fixture().CreateMany<Vector3>(count).ToList();

        return new DefaultBridgeStrategy().Bridge(input, 1);
    }
}
