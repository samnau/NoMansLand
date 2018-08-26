using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Motion_Controller : MonoBehaviour {
    private float yPos;
    private float originalYPos;
    private float randomY;
    private bool isVisible;
    private float xPos;
    private Camera myCamera;
    private float cloudWidth;
    private float originalXScale;
    private float originalYScale;
    private float motionDistance;

	// Use this for initialization
	void Start () {
        yPos = originalYPos = transform.position.y;
        xPos = transform.position.x;
        cloudWidth = GetComponent<SpriteRenderer>().size.x;
        originalXScale = transform.localScale.x;
        originalYScale = transform.localScale.y;
        motionDistance = Random.Range(0.001f, 0.005f);
	}

    private void moveObejct()
    {
        var newXPos = xPos += motionDistance;
        var newVector = new Vector2(newXPos, yPos);
        transform.position = newVector;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
        resetPositon();
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private float randomYMaker()
    {
        var randomAdjuster = Random.Range(-1f, 1f);
        return originalYPos + randomAdjuster;
    }

    private Vector3 randomScaler()
    {
        var randomAdjuster = Random.Range(-0.2f, 0.2f);
        var newYScale = originalYScale + randomAdjuster;
        var newXScale = originalXScale + randomAdjuster;
        return new Vector3(newXScale, newYScale, 1);
    }

    private void resetPositon()
    {
        var screenVector = new Vector3(Screen.width, Screen.height, 0);
        Vector3 viewDimenions = Camera.main.ScreenToWorldPoint(screenVector);
        var newXpos = (viewDimenions.x + cloudWidth) * -1f;
        transform.position = new Vector2(newXpos, randomYMaker());
        xPos = transform.position.x;
        yPos = transform.position.y;
        transform.localScale = randomScaler();
    }

    // Update is called once per frame
    void Update () {
        moveObejct();
	}
}
