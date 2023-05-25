using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BaseTweener : MonoBehaviour
{
    [SerializeField]
    public float speed = 1.5f;
    [HideInInspector]
    public float progress = 0f;

    protected float EaseOutQuad(float t) => 1 - EaseInQuad(1 - t);
    protected float EaseInQuad(float t)
    {
        return t * t;
    }
    protected float EaseInOutQuad(float t)
    {
        if (t < 0.5) return EaseInQuad(t * 2) / 2;
        return 1 - EaseInQuad((1 - t) * 2) / 2;
    }

    protected float EaseOutSine(float x) {
      return Mathf.Sin((x* Mathf.PI) / 2);
    }

    protected float EaseInElastic(float t) => 1 - EaseOutElastic(1 - t);
    public static float EaseOutElastic(float t)
    {
        var c4 = (2 * Mathf.PI) / 3;
        return t == 0
          ? 0
          : t == 1
          ? 1
          : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1;
    }
    protected float EaseInOutElastic(float t)
    {
        if (t < 0.5) return EaseInElastic(t * 2) / 2;
        return 1 - EaseInElastic((1 - t) * 2) / 2;
    }
    protected float EaseOutQuint(float x) {
     return 1 - Mathf.Pow(1 - x, 5);
    }

    protected float EaseInQuint(float x) {
        return x* x * x* x * x;
    }
}

