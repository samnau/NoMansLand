using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobalDataPersistence
{
    //This file is just to enforce that any file that will load and save something in the data will have the right methods for save/load
    void LoadData(GlobalGameData data);

    void SaveData(ref GlobalGameData data);
}
