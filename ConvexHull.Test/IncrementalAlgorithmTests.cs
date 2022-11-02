using System.Numerics;
using ConvexHull.Algorithms;
using FluentAssertions;
using FluentAssertions.Execution;

namespace ConvexHull.Test;

public class IncrementalAlgorithmTests
{
    [Theory, ClassData(typeof(AlgorithmTestData))]
    public void IncrementalAlgorithm(List<Vector3> input, List<Vector3> expected)
    {
        IConvexHullAlgorithm algorithm = new IncrementalAlgorithm();

        var result = algorithm.Compute(input);

        using var scope = new AssertionScope();

        result.Should().BeEquivalentTo(expected);
    }
}
