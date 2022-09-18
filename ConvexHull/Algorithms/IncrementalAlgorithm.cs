namespace ConvexHull.Algorithms;

public class IncrementalAlgorithm : IConvexHullAlgorithm
{
    public List<Vector3> Compute(List<Vector3> input)
    {
        input.Sort(Helper.EdgeCompareX);
        
        var upperHull = ComputeUpperHull(input);
        var lowerHull = ComputeLowerHull(input);

        return upperHull.Concat(lowerHull).Distinct().ToList();
    }

    private static IEnumerable<Vector3> ComputeLowerHull(List<Vector3> input)
    {
        input.Reverse();

        return ComputeUpperHull(input);
    }
    private static IEnumerable<Vector3> ComputeUpperHull(List<Vector3> input)
    {
        var output = new List<Vector3>();
        
        foreach (var v in input)
        {
            output.Add(v);

            while (output.Count > 2 && !Helper.LastRightTurn(output))
            {
                output.RemoveAt(output.Count - 2);
            }
        }

        return output;
    }
}
