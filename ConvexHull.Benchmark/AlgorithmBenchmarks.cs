using System.Numerics;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using ConvexHull.Algorithms;

namespace ConvexHull.Benchmark;

public class AlgorithmBenchmarks
{
    private readonly IConvexHullAlgorithm incrementalAlgorithm = new IncrementalAlgorithm();
    private readonly IConvexHullAlgorithm naiveAlgorithm = new NaiveAlgorithm();
    private readonly IConvexHullAlgorithm kirkpatrickSeidelAlgorithm = new KirkpatrickSeidelAlgorithm();

    [Benchmark, Arguments(10), Arguments(100), Arguments(1000)]
    public List<Vector3> KirkpatrickSeidelAlgorithm(int count)
    {
        var input = new Fixture().CreateMany<Vector3>(count).ToList();
        return kirkpatrickSeidelAlgorithm.Compute(input);
    }
    
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
}
