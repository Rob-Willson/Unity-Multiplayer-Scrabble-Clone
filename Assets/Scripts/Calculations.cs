using UnityEngine;

public static class Calculations
{
    /// <summary>
    /// Determine whether two float values are similar according to a certain threshold
    /// </summary>
    public static bool Approximately (float a, float b, float threshold)
    {
        return ((a < b) ? (b - a) : (a - b)) <= threshold;
    }

    /// <summary>
    /// Determine whether two Vector2s are similar according to a certain threshold
    /// </summary>
    public static bool Approximately (Vector2 a, Vector2 b, float threshold)
    {
        if(Approximately(a.x, b.x, threshold) && Approximately(a.y, b.y, threshold))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Determine whether two Vector3s are similar according to a certain threshold
    /// </summary>
    public static bool Approximately (Vector3 a, Vector3 b, float threshold)
    {
        if(Approximately(a.x, b.x, threshold) && Approximately(a.y, b.y, threshold) && Approximately(a.z, b.z, threshold))
        {
            return true;
        }
        return false;
    }

}