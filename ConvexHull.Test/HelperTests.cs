using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;

namespace ConvexHull.Test;

public class HelperTests
{
    [Fact]
    public void FindMinMax()
    {
        var input = new List<Vector3>
        {
            new(3, 0, 0),
            new(1, 1, 0),
            new(0, 0, 0),
            new(2, 0, 0),
            new(1, 0, 0),
            new(2, 1, 0),
            new(1, -1, 0),
            new(2, -1, 0),
        };

        var (min, max) = Helper.FindMinMax(input);

        using var scope = new AssertionScope();
        
        min.Should().Be(new Vector3(0, 0, 0));
        max.Should().Be(new Vector3(3, 0, 0));
    }

    [Fact]
    public void SortClockwise()
    {
        var input = new List<Vector3>
        {
            new(2, 1, 0),
            new(1, -1, 0),
            new(3, 0, 0),
            new(1, 1, 0),
            new(0, 0, 0),
            new(2, -1, 0),
        };

        var sortClockwise = Helper.SortClockwise(input);

        var expected = new List<Vector3>
        {
            new(0, 0, 0),
            new(1, 1, 0),
            new(2, 1, 0),
            new(3, 0, 0),
            new(2, -1, 0),
            new(1, -1, 0),
        };

        sortClockwise.Should().BeEquivalentTo(expected, options => options.WithStrictOrderingFor(vector3 => vector3));
    }

    [Fact]
    public void IsToTheLeft_Positive()
    {
        var r = new Vector3(1, 1, 0);
        var p = new Vector3(3, 3, 0);
        var q = new Vector3(2, 4, 0);

        var isToTheLeft = Helper.IsToTheLeft(r, p, q);

        isToTheLeft.Should().BePositive();
    }

    [Fact]
    public void IsToTheLeft_On()
    {
        var r = new Vector3(1, 1, 0);
        var p = new Vector3(3, 3, 0);
        var q = new Vector3(2, 2, 0);

        var isToTheLeft = Helper.IsToTheLeft(r, p, q);

        isToTheLeft.Should().BeApproximately(0.0, double.Epsilon);
    }

    [Fact]
    public void IsToTheLeft_Negative()
    {
        var r = new Vector3(1, 1, 0);
        var p = new Vector3(3, 3, 0);
        var q = new Vector3(2, 1, 0);

        var isToTheLeft = Helper.IsToTheLeft(r, p, q);

        isToTheLeft.Should().BeNegative();
    }

    [Fact]
    public void Shuffle()
    {
        var input = new List<int>
            {1,2,3,4,5,6,7,8,9};

        var shuffled = new List<int>(input);
        shuffled.Shuffle();

        input.Should().BeEquivalentTo(shuffled);
        input.Should().NotBeSameAs(shuffled);
    }
}