using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTweener : MonoBehaviour {
    public bool scaleMeUp = false;
    public bool scaleMeDown = true;
    Transform targetTransform;
    public float targetScaleFloat = 1.5f;
    Vector3 targetScale;
    Vector3 initialScale;
    Vector3 currentScale;
    float changeIncrement = 0;
    public float speed = 0.25f;

	// Use this for initialization
	void Start () {
        targetTransform = gameObject.transform;
        initialScale = targetTransform.localScale;
        targetScale = initialScale + new Vector3(targetScaleFloat, targetScaleFloat, targetScaleFloat);
	}
    void findCurrentScale()
    {
        currentScale = targetTransform.localScale;
    }
	void ScaleUp ()
    {
        changeIncrement += (Time.deltaTime * speed);
        findCurrentScale();
        if (changeIncrement < 1.0f)
        {
            transform.localScale = Vector3.Lerp(currentScale, targetScale, changeIncrement);
        }else
        {
            scaleMeUp = false;
            changeIncrement = 0;
        }
    }
    void ScaleDown()
    {
        changeIncrement += (Time.deltaTime * speed);
        findCurrentScale();
        if (changeIncrement < 1.0f)
        {
            transform.localScale = Vector3.Lerp(currentScale, initialScale, changeIncrement);
        }
        else
        {
            scaleMeDown = false;
            changeIncrement = 0;
        }
    }
    // Update is called once per frame
    void Update () {
		if(scaleMeUp)
        {
            ScaleUp();
        }

        if (scaleMeDown)
        {
            ScaleDown();
        }
    }
}
