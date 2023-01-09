using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField]
    string fileName;

    List<IDataPersistance> dataPersistanceObjects;
    GameData gameData;
    public static DataPersistanceManager instance { get; private set; }

    FileDataHandler dataHandler;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one data manager found");
        }

        instance = this;
    }

    private void Start()
    {
        print(Application.persistentDataPath);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // TODO: Load saved data
        this.gameData = dataHandler.Load();
        if(this.gameData == null)
        {
            print("NO Data found. Starting with defaults");
            NewGame();
        }

        // TODO push loaded data to the system
        foreach(IDataPersistance dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.LoadData(gameData);
        }

        print($"Loaded days passed {gameData.dayCount}");
    }

    public void SaveGame()
    {
        // passes data to save sripts
        // saves data to a file handler
        foreach (IDataPersistance dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.SaveData(ref gameData);
            print($"Saved days passed {gameData.dayCount}");
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
