namespace ConvexHull.Algorithms;

public interface IBridgeStrategy
{
    (Vector3 i, Vector3 j) Bridge(List<Vector3> S, float a);
}
