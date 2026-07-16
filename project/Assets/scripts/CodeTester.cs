using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTester : MonoBehaviour
{
    [SerializeField] InputStateTracker inputStateTracker;
    [SerializeField] HeroMotionController heroMotionController;
    // Start is called before the first frame update

    public void FreezePlayer()
    {
        if(!inputStateTracker || !heroMotionController)
        {
            Debug.LogWarning("missing assignments");
            return;
        }
        print("code test: freeze player");
        //inputStateTracker.isUiActive = true;
       // inputStateTracker.enabled = false;
       // heroMotionController.enabled = false;
        inputStateTracker.DisableMovement();
        heroMotionController.DisableMovement();
    }

    public void UnfreezePlayer()
    {
        if (!inputStateTracker || !heroMotionController)
        {
            Debug.LogWarning("missing assignments");
            return;
        }
        print("code test: unfreeze player");
        //inputStateTracker.isUiActive = false;
        //inputStateTracker.enabled = true;
       // heroMotionController.enabled = true;
        inputStateTracker.EnableMovement();
        heroMotionController.EnableMovement();
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }

    public void ShowObject()
    {
        gameObject.SetActive(true);
    }

    public void  LogTestMessage()
    {
        print("This is a code tester message");
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FreezePlayer();
        //}

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    UnfreezePlayer();
        //}

    }
}
