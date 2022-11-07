﻿namespace ConvexHull;

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = RandomProvider.Random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
