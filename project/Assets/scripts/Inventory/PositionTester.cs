using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTester : MonoBehaviour
{
    [SerializeField]
    GameObject positionPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPanelPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        print($"mouse: {mousePosition}");
        mousePosition.z = Camera.main.nearClipPlane; // Use a fixed depth
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (positionPanel)
        {
            positionPanel.transform.position = worldPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
