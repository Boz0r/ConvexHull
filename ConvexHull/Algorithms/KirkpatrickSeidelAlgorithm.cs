namespace ConvexHull.Algorithms;

public class Pair
{
    public Vector3 i;
    public Vector3 j;
    public float slope;

    public Pair(Vector3 i, Vector3 j)
    {

        this.i = i;
        this.j = j;
    }

    public static Pair CreateSorted(Vector3 i, Vector3 j)
    {
        return i.X <= j.X ? new Pair(i, j) : new Pair(j, i);
    }
}

public class KirkpatrickSeidelAlgorithm : IConvexHullAlgorithm
{
    private readonly IBridgeStrategy _defaultBridgeStrategy;

    public KirkpatrickSeidelAlgorithm()
    {
        _defaultBridgeStrategy = new DefaultBridgeStrategy();
    }

    public List<Vector3> Compute(List<Vector3> input)
    {
        var upperHull = ComputeUpperHull(input);
        var lowerHull = ComputeLowerHull(input);

        return upperHull.Concat(lowerHull).Distinct().ToList();
    }

    private IEnumerable<Vector3> ComputeLowerHull(List<Vector3> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            var v = input[i];

            input[i] = new Vector3(-v.X, -v.Y, -v.Z);
        }

        var computeUpperHull = ComputeUpperHull(input).ToList();

        for (int i = 0; i < computeUpperHull.Count; i++)
        {
            var v = computeUpperHull[i];

            computeUpperHull[i] = new Vector3(-v.X, -v.Y, -v.Z);
        }

        return computeUpperHull;
    }

    private IEnumerable<Vector3> ComputeUpperHull(List<Vector3> input)
    {
        if (input.Count <= 0)
        {
            return input;
        }

        var output = new List<Vector3>();

        var (min, max) = Helper.FindMinMax(input);

        if (min == max)
        {
            output.Add(min);
            return output;
        }

        var T = new List<Vector3>
            { min, max };
        T.AddRange(input.Where(p => p.X > min.X && p.X < max.X));

        return Connect(min, max, T).ToList();
    }

    private IEnumerable<Vector3> Connect(Vector3 min, Vector3 max, List<Vector3> S)
    {
        var output = new List<Vector3>();

        var a = S[RandomProvider.Random.Next(0, S.Count)].X;

        var (i, j) = _defaultBridgeStrategy.Bridge(S, a);

        var sLeft = new List<Vector3>
            { i };
        sLeft.AddRange(S.Where(p => p.X < i.X));

        var sRight = new List<Vector3>
            { j };
        sRight.AddRange(S.Where(p => p.X > j.X));

        if (i == min)
        {
            output.Add(i);
        }
        else
        {
            output.AddRange(Connect(min, i, sLeft));
        }

        if (j == max)
        {
            output.Add(j);
        }
        else
        {
            output.AddRange(Connect(j, max, sRight));
        }

        return output;
    }
}
