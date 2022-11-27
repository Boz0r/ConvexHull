using System.Numerics;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Abstractions;

namespace ConvexHull.Test;

public class DualityTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DualityTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test()
    {
        Line2D line = new Line2D(0.5f, 2);

        List<Vector3> vectors = new List<Vector3>
        {
            new(4, 4, 0),
            new (-4, 0, 0),
            new(0, 2, 0)
        };

        using var scope = new AssertionScope();
        
        var dualMap2D = line.DualMap2D();
        _testOutputHelper.WriteLine(dualMap2D.ToString());
    
        foreach (var vector in vectors)
        {
            var line2D = vector.DualMap2D();
            _testOutputHelper.WriteLine(line2D.ToString());

            
            dualMap2D.Should().Intersect(line2D).And.NotBeNull();
            dualMap2D.Intersects(line2D).Should().BeTrue();
            line.Intersects(vector).Should().BeTrue();
        }
    }

    [Theory, AutoData]
    public void Message_Action_Expectation(Vector3 p, Line2D l)
    {

        
    }
}