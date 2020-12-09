using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float ClampedPercent(float numerator, float denominator)
    {
        return Mathf.Clamp(numerator / denominator, 0f, 1f);
    }
}
