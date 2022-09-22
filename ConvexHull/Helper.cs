namespace ConvexHull;

public static class Helper
{

    public static bool LastRightTurn(List<Vector3> list)
        => !(IsToTheLeft(list[^2], list[^1], list[^3]) >= 0);

    /// <summary>
    ///     Returns positive if point r is to the left of the line through p and q. Also returns true if all points are on a
    ///     line. Only works in 2D.
    /// </summary>
    public static double IsToTheLeft(Vector3 r, Vector3 p, Vector3 q)
        => (q.X - p.X) * (r.Y - p.Y) - (q.Y - p.Y) * (r.X - p.X);

    /// <summary>
    ///     Sorts vectors by x coordinate
    /// </summary>
    public static int EdgeCompareX(Vector3 x, Vector3 y)
        => x.X.CompareTo(y.X);

    /// <summary>
    /// </summary>
    public static List<Vector3> SortClockwise(List<Vector3> input)
    {
        return input.OrderBy(x => Math.Atan2(x.X, x.Y)).ToList();
    }
}
