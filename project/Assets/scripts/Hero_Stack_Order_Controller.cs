using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Hero_Stack_Order_Controller : MonoBehaviour
{
    private float Ypos;
    void StackSetter()
    {
        Ypos = transform.position.y;
        var heroSorter = GetComponent<SortingGroup>();
        var newOrder = Mathf.RoundToInt(Ypos * -100f);
        heroSorter.sortingOrder = newOrder;
    }

    void Start()
    {
        StackSetter();
    }
    // Update is called once per frame
    void Update()
    {
        StackSetter();
    }
}
