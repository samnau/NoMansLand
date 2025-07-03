using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
        print("game data loaded with scene load");
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one persistence manager found. Destroying the old one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        this.dataHandler = new GlobalFileDataHandler(Application.persistentDataPath, fileName);
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        //this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        //LoadGame();
    }

    public void NewGame()
    {
        this.globalGameData = new GlobalGameData();
    }

    public void LoadGame()
    {
        // NOTE: load data with file handler
        this.globalGameData = dataHandler.Load();
        // if no data, init the game data with defaults
        if(this.globalGameData == null)
        {
            print("No game data found. Initializing new game");
            NewGame();
        }

        // NOTE: push loaded data to all code that needs it
        foreach(IGlobalDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(globalGameData);
        }
    }

    public void SaveGame()
    {
        // NOTE: pass data to scripts so they can update it
        // NOTE: save data to file using data handler

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
