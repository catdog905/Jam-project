using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMath
{
    public static Vector2 rotate(Vector2 v, float degrees) {
        degrees *= Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(degrees) - v.y * Mathf.Sin(degrees),
            v.x * Mathf.Sin(degrees) + v.y * Mathf.Cos(degrees)
        );
    }
}
