using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IDataPersistance
{
    private int dayCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadData(GameData data)
    {
        this.dayCount = data.dayCount;
    }

    public void SaveData(GameData data)
    {
        data.dayCount = this.dayCount;
    }

    void IncreaseDayCount()
    {
        dayCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            IncreaseDayCount();
        }
    }
}
