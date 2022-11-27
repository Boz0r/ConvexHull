namespace ConvexHull;

/// <summary>
/// Class for representing a line in the form y = ax + b
/// </summary>
public class Line2D
{
    public float A { get; }
    public float B { get; }

    public Line2D(float a, float b)
    {
        A = a;
        B = b;
    }

    public bool Intersects(Vector3 vector)
    {
        return Math.Abs(vector.Y - (A * vector.X + B)) < 0.001f;
    }

    public override string ToString()
    {
        return $"y = {A}x + {B}";
    }
}
