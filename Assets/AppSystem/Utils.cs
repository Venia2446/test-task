using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float CalculateRotationAngleRad(Vector3 target1, Vector3 target2)
    {
        var dir = target1 - target2;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public static Quaternion CalculateRotationAngle(Vector3 tarter1, Vector3 target2)
    {
        return Quaternion.Euler(0f, 0f, CalculateRotationAngleRad(tarter1, target2));
    }
}
