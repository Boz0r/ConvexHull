namespace ConvexHull.Algorithms;

/// <summary>
///     Takes all pairs (a, b) of points as vectors and adds a to the hull if all other points are on the same side.
///     O(n^3)
/// </summary>
public class NaiveAlgorithm : IConvexHullAlgorithm
{
    public List<Vector3> Compute(List<Vector3> input)
    {
        var output = new List<Vector3>();

        foreach (var a in input)
        {
            foreach (var b in input.Where(x => x != a))
            {
                if (Valid(input, a, b))
                {
                    output.Add(a);
                }
            }
        }

        return output;
    }

    private static bool Valid(List<Vector3> input, Vector3 a, Vector3 b)
    {
        foreach (var c in input.Where(x => x != a && x != b))
        {
            var isToTheLeft = Helper.IsToTheLeft(a, b, c);
            if (isToTheLeft > 0)
            {
                return false;
            }
        }

        return true;
    }
}
