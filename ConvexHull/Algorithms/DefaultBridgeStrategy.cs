namespace ConvexHull.Algorithms;

public class DefaultBridgeStrategy : IBridgeStrategy
{
    public (Vector3 i, Vector3 j) Bridge(List<Vector3> S, float a)
    {
        var candidates = new List<Vector3>();

        //  1. If |S| = 2 then return ((i,j)), where S={p,,pj} and x(p,)<x(pj).
        if (S.Count == 2)
        {
            S.Sort(Helper.EdgeCompareX);
            return (S[0], S[1]);
        }

        var sIndicators = new List<Vector3>(S);
        sIndicators.Shuffle();

        if (sIndicators.Count % 2 != 0)
        {
            candidates.Add(sIndicators.Last());
        }

        var pairs = new List<Pair>();

        for (int i = 1; i < sIndicators.Count; i += 2)
        {
            pairs.Add(Pair.CreateSorted(sIndicators[i - 1], sIndicators[i]));
        }

        // 3. Determine the slopes of the straight lines defined by the pairs.
        foreach (var pair in new List<Pair>(pairs))
        {
            if (Math.Abs(pair.i.X - pair.j.X) < float.Epsilon)
            {
                pairs.Remove(pair);

                var candidateToAdd = pair.i.Y > pair.j.Y ? pair.i : pair.j;

                candidates.Add(candidateToAdd);
            }
            else
            {
                pair.slope = K(pair.i, pair.j);
            }
        }

        if (pairs.Count > 0)
        {
            // 4. Determine K, the median of {k(p, P)I(P, P) PAIRS}
            // Randomly choose an element (p_i, p_j) from PAIRS such that the choice of every element is equally likely, and let K <- k(p_i, p_j)
            var k = pairs[RandomProvider.Random.Next(0, pairs.Count - 1)].slope;

            var small = pairs.Where(pair => pair.slope < k).ToList();
            var equal = pairs.Where(pair => Math.Abs(pair.slope - k) < 0.00001).ToList();
            var large = pairs.Where(pair => pair.slope > k).ToList();

            // 6. Find the set of points of S which lie on the supporting line h with slope K
            float maxPointSlope = float.MinValue;
            foreach (var vector3 in S)
            {
                var pointSlope = vector3.Y - k * vector3.X;
                maxPointSlope = Math.Max(maxPointSlope, pointSlope);
            }

            var max = S.Where(vector3 => Math.Abs(vector3.Y - k * vector3.X - maxPointSlope) < 0.00001).ToList();

            var p_k = max.MinBy(vector3 => vector3.X);
            var p_m = max.MaxBy(vector3 => vector3.X);

            // 7. Determine if h contains the bridge
            if (p_k.X <= a && p_m.X > a)
            {
                return (p_k, p_m);
            }

            // 8. h contains only points to the left of or on L
            if (p_m.X <= a)
            {
                candidates.AddRange(large.Select(pair => pair.j));
                candidates.AddRange(equal.Select(pair => pair.j));
                candidates.AddRange(small.Select(pair => pair.i));
                candidates.AddRange(small.Select(pair => pair.j));
            }

            // 9. h contains only points to the right of L
            if (p_k.X > a)
            {
                candidates.AddRange(small.Select(pair => pair.i));
                candidates.AddRange(equal.Select(pair => pair.i));
                candidates.AddRange(large.Select(pair => pair.i));
                candidates.AddRange(large.Select(pair => pair.j));
            }
        }

        return Bridge(candidates, a);
    }


    private float K(Vector3 i, Vector3 j)
    {
        return (i.Y - j.Y) / (i.X - j.X);
    }
}
