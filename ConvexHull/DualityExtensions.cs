namespace ConvexHull;

public static class DualityExtensions
{
    public static Vector3 DualMap2D(this Line2D line)
    {
        return new Vector3(line.A, -line.B, 0);
    }

    public static Line2D DualMap2D(this Vector3 vector)
    {
        return new Line2D(vector.X, -vector.Y);
    }

    public static bool Intersects(this Line2D line, Vector3 vector)
    {
        return Math.Abs(vector.Y - (line.A * vector.X + line.B)) < 0.001f;
    }

    public static bool Intersects(this Vector3 vector, Line2D line)
    {
        return Math.Abs(vector.Y - (line.A * vector.X + line.B)) < 0.001f;
    }
}
