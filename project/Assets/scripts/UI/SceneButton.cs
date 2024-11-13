using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    Fade_Controller fadeController;
    [SerializeField]
    string targetScene = "Forest1";
    // Start is called before the first frame update
    void Start()
    {
        fadeController = FindObjectOfType<Fade_Controller>();
        Button button = gameObject.GetComponent<Button>();
    }

    public void ChangeScene(string targetScene)
    {
        fadeController.triggerLevelChange(targetScene);
    }

}
