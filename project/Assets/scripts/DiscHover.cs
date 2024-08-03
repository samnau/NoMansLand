using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscHover : MonoBehaviour
{
    //public AnimationCurve floatCurve;
    //adjust this to change speed
    [SerializeField]
    float speed = 2f;
    //adjust this to change how high it goes
    [SerializeField]
    float height = 0.02f;
    bool delayComplete = false;
    [SerializeField]
    float delayAmount = 0.25f;

    Vector3 pos;
    private void Start()
    {
        pos = transform.position;
        StartCoroutine("DelayStart");
    }

    IEnumerator DelayStart()
    {
        if(!delayComplete)
        {
            yield return new WaitForSeconds(delayAmount);
        }
        delayComplete = true;
    }
    void SetFloat()
    {
        if(delayComplete)
        {
            float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
    void Update()
    {
        SetFloat();
        //calculate what the new Y position will be
        //float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        //set the object's Y to the new calculated Y
        //transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        // transform.position = new Vector3(transform.position.x, floatCurve.Evaluate((Time.time % floatCurve.length)), transform.position.z);
    }
}
