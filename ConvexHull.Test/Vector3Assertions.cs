using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace ConvexHull.Test;

public class Vector3Assertions : Vector3Assertions<Vector3Assertions>
{
    public Vector3Assertions(Vector3 value)
        : base(value)
    {
    }
}

public class Vector3Assertions<TAssertions> : ObjectAssertions<Vector3, TAssertions>
    where TAssertions : Vector3Assertions<TAssertions>
{
    protected Vector3Assertions(Vector3 value) : base(value)
    {
    }
    
    public AndConstraint<TAssertions> Intersect(Line2D expected, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(Subject.Intersects(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:Vector3} to intersect with {0}{reason}, but found {1}.", expected, Subject);
        
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}

public static class AssertionExtensions
{
    public static Vector3Assertions Should(this Vector3 actualValue)
    {
        return new Vector3Assertions(actualValue);
    }
}