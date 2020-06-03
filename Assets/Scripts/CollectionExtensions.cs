using UnityEngine;
using System.Collections.Generic;

public static class CollectionExtensions
{
    /// <summary>
    /// Randomly shuffle a collection.
    /// Based on a Fisher-Yates shuffle, relying on Random.Range().
    /// So it's fast but not actually particularly random (but good enough for most uses).
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while(n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}