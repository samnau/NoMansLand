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
}
