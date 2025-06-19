using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalDataPersistenceManager : MonoBehaviour
{
    //NOTES ******
    // This file:
    // 1. allows you to set the name of your save file
    // 2. Implements Saving, Loading and Starting a new game with an instance of the defined GlobalGameData class
    // 3. Finds any script that implements the global data persistence interface
    // 4. Then each instance of that interface will pass data to be saved or or recieve data to be loaded, depending on the situation
    [Header("File Storage Config")]
    [SerializeField] string fileName;

    GlobalGameData globalGameData;

    GlobalFileDataHandler dataHandler;

    List<IGlobalDataPersistence> dataPersistenceObjects;
    public static GlobalDataPersistenceManager instance { get; private set;  }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one persistence manager found");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new GlobalFileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.globalGameData = new GlobalGameData();
    }

    public void LoadGame()
    {
        // todo: load data with file handler
        this.globalGameData = dataHandler.Load();
        // if no data, init the game data with defaults
        if(this.globalGameData == null)
        {
            print("No game data found. Initializing new game");
            NewGame();
        }

        // todo: push loaded data to all code that needs it
        foreach(IGlobalDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(globalGameData);
        }
        print($"Game Loaded - day count: {globalGameData.gameState["dayCount"]}");

    }

    public void SaveGame()
    {
        // todo: pass data to scripts so they can update it
        // todo: save data to file using data handler

        foreach (IGlobalDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref globalGameData);
        }

        dataHandler.Save(globalGameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    List<IGlobalDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IGlobalDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IGlobalDataPersistence>();
        return new List<IGlobalDataPersistence>(dataPersistenceObjects);
    }
}
