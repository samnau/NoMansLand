using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSweeperTargetController : BattleChallenge
{
    float rotationMultiplier;
    float rotationModfier = 1f;
    float rotationBase = 90f;
    float targetRotaton;
    public GameObject battleTrigger;
    int hitCount = 0;
    bool hitActive = false;
    public int hitSuccessLimit = 4;
    public float triggerTimeLimit = .75f;
    ColorTweener colorTweener;

    void Start()
    {
        SetTargetRotation();
        StartCoroutine(SetRotation());
    }

    void SetTargetRotation()
    {
        var randomModfier = Random.Range(0, 10);
        rotationMultiplier = Random.Range(1, 4) * 1f;
        rotationModfier = randomModfier < 5 ? -1f : 1f;
        targetRotaton = rotationBase * rotationMultiplier * rotationModfier;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == battleTrigger.name)
        {
            print("sweeper trigger");
            hitActive = true;
        }

        if (collision.CompareTag("BattleTrigger"))
        {
            ColorTweener targetTweener = collision.gameObject.GetComponent<ColorTweener>();
            targetTweener.TriggerAlphaImageTween(1f, 10);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == battleTrigger.name)
        {
            print("sweeper trigger");
            hitActive = false;
        }
        if (collision.CompareTag("BattleTrigger"))
        {
            ColorTweener targetTweener = collision.gameObject.GetComponent<ColorTweener>();
            targetTweener.TriggerAlphaImageTween(0.5f, 10);
        }
    }
    IEnumerator SetRotation()
    {
        yield return new WaitForSeconds(triggerTimeLimit);
        SetTargetRotation();
        transform.Rotate(0, 0, targetRotaton);
        yield return new WaitForSeconds(triggerTimeLimit);
        StartCoroutine(SetRotation());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) && hitActive)
        {
            hitCount++;
            print($"hit: {hitCount}");

            if (hitCount >= hitSuccessLimit)
            {
                print("YOU WIN!!!");
            }
        }
    }
}
