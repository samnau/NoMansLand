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

    // Start is called before the first frame update
    void Start()
    {
        //rotationMultiplier = Random.Range(1, 4) * 1f;
        SetTargetRotation();
        StartCoroutine(SetRotation());
        //rotationModfier = Random.Range(-1, 1);
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == battleTrigger.name)
        {
            print("sweeper trigger");
            hitActive = false;
        }
    }
    IEnumerator SetRotation()
    {
        yield return new WaitForSeconds(1.0f);
        SetTargetRotation();
        transform.Rotate(0, 0, targetRotaton);
        StartCoroutine(SetRotation());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) && hitActive)
        {
            print("hit");
            print(hitCount);
            hitCount++;
        }
    }
}
