using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSpinnerButton : MonoBehaviour
{
    Button button;
    bool clickValid = false;
    bool keyValid = false;
    BattleSpinner battleSpiner;
    // Start is called before the first frame update
    void Start()
    {
        battleSpiner = FindObjectOfType<BattleSpinner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if(isBattleTrigger)
        {
            clickValid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var isBattleTrigger = collision.CompareTag("BattleTrigger");
        if (isBattleTrigger)
        {
            clickValid = false;
        }
    }

    bool IsKeyValid()
    {
        return Input.GetKeyDown(KeyCode.D);
    }

    public void checkForValidClick()
    {
        if(clickValid)
        {
            print("key success!");
            battleSpiner.ReverseRotation();
        } else
        {
            print("key fail!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsKeyValid())
        {
            checkForValidClick();
        }
    }
}
