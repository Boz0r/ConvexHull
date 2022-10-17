using System.Numerics;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using ConvexHull.Algorithms;

namespace ConvexHull.Benchmark;

public class Benchmarks
{
    private readonly IConvexHullAlgorithm incrementalAlgorithm = new IncrementalAlgorithm();
    private readonly IConvexHullAlgorithm naiveAlgorithm = new NaiveAlgorithm();
    private readonly IBridgeStrategy bridgeStrategy = new DefaultBridgeStrategy();

    [Benchmark, Arguments(10), Arguments(100), Arguments(1000)]
    public List<Vector3> IncrementalAlgorithm(int count)
    {
        var input = new Fixture().CreateMany<Vector3>(count).ToList();
        return incrementalAlgorithm.Compute(input);
    }
    
    [Benchmark, Arguments(10), Arguments(100), Arguments(1000)]
    public List<Vector3> NaiveAlgorithm(int count)
    {
        var input = new Fixture().CreateMany<Vector3>(count).ToList();
        return naiveAlgorithm.Compute(input);
    }

    [Benchmark, Arguments(1), Arguments(10), Arguments(100), Arguments(1000)]
    public (Vector3 i, Vector3 j) DefaultBridgeStrategy(int count)
    {
        var input = new Fixture().CreateMany<Vector3>(count).ToList();

        return bridgeStrategy.Bridge(input, 1);
    }
}
