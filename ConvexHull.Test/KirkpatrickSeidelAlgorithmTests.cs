﻿using System.Numerics;
using ConvexHull.Algorithms;
using FluentAssertions;
using FluentAssertions.Execution;

namespace ConvexHull.Test;

public class KirkpatrickSeidelAlgorithmTests
{
    [Fact]
    public void Bridge()
    {
        var input = new List<Vector3>
        {
            new(3, 6, 0),
            new(0, 5, 0),
            new(10, 5, 0),
        };

        DefaultBridgeStrategy sut = new DefaultBridgeStrategy();
        
        var (i, j) = sut.Bridge(input, 3);
        
        i.Should().Be(new Vector3(3, 6, 0));
        j.Should().Be(new Vector3(10, 5, 0));
    }   
    
    [Fact]
    public void Bridge2()
    {
        var input = new List<Vector3>
        {
            new(1, 2, 0),
            new(1, 3, 0),
            new(0, 1, 0),
        };

        DefaultBridgeStrategy sut = new DefaultBridgeStrategy();
        
        var (i, j) = sut.Bridge(input, .5f);

        using var scope = new AssertionScope();
        
        i.Should().Be(new Vector3(0, 1, 0));
        j.Should().Be(new Vector3(1, 3, 0));
    }

    [Theory]
    [RepeatClassData(10, typeof(AlgorithmTestData))]
    public void KirkpatrickSeidelAlgorithm(int iteration, List<Vector3> input, List<Vector3> expected)
    {
        RandomProvider.Random = new Random(iteration);
        IConvexHullAlgorithm algorithm = new KirkpatrickSeidelAlgorithm();

        var result = algorithm.Compute(input);

        using var scope = new AssertionScope();

        result.Should().BeEquivalentTo(expected);
    }
}