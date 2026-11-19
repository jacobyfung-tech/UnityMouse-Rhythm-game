using UnityEngine;

/// helper for spreading per-object work across frames
public static class Stagger
{

    static int _divisor = 1;


    public static bool Tick(Transform t) => Tick(t, _divisor);

    public static bool Tick(Transform t, int divisor)
    {
        if (divisor <= 1) return true;
        return (t.GetSiblingIndex() + Time.frameCount) % divisor == 0;
    }


    public static bool Tick(int id, int divisor = -1)
    {
        if (divisor < 0) divisor = _divisor;
        if (divisor <= 1) return true;
        return (id + Time.frameCount) % divisor == 0;
    }
}