using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class GlobalGameData
{
    // This file is the Schema for your save data structure
    // Use it as the source of truth for all game data
    public int dayCount;
    public int deathCount;

    public SerializableDictionary<string, string> gameState;
    public class GameStateDefault
    {
        public bool gameComplete { get; set; }
        public bool gameInProgress { get; set; }
        public string currentScene { get; set; }
        public string lastDirection { get; set; }
        public int dayCount { get; set; }
    }

    [System.Serializable]
    public class WorldState
    {
        public bool gameComplete;
        public bool gameInProgress;
        public string currentScene;
        public string lastDirection;
        public int dayCount;
    }

    [System.Serializable]
    public class GameSettings
    {
        public float musicVolume;
        public float fxVolume;
        public string controller;
    }

    public class OneTimeEvents
    {
        public string castleCourtyard { get; set; }
        public string castleThroneRoom { get; set; }
    }

    public class InventoryItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public bool collected { get; set; }
        public string imageId { get; set; }
    }

    public class Familiar : InventoryItem
    {
        public string weakness { get; set; }
    }

    public class EnvironmentState
    {
        public bool castleWalls { get; set; }
        public bool castleFloatingStairs { get; set; }
        public bool castleCourtYardRightDoor { get; set; }
        public bool forestBridge { get; set; }
    }

    // TODO: working list of real image assets listed by a key string
    // public Dictionary<string, Image> itemImageLibrary

    public GameStateDefault gameStateDefault;
    public GameSettings gameSettings;
    public WorldState worldState;

    // NOTE: example code for iterating over properties of a class
    // https://stackoverflow.com/questions/8151888/c-sharp-iterate-through-class-properties
    // TODO: adapt this to populate SerializableDictionary
    //Record record = new Record();

    //PropertyInfo[] properties = typeof(Record).GetProperties();
    //foreach (PropertyInfo property in properties)
    //{
    //    property.SetValue(record, value);
    //}
    
    // Values in the constructor below will be the default values for any new instance of the save data
    public GlobalGameData()
    {
        dayCount = 0;
        deathCount = 0;
        gameStateDefault = new GameStateDefault
        { 
            gameInProgress = false, 
            gameComplete = false,
            currentScene = "",
            lastDirection = "right",
            dayCount = 0
        };

        worldState = new WorldState
        {
            gameInProgress = false,
            gameComplete = false,
            currentScene = "",
            lastDirection = "right",
            dayCount = 0
        };

        gameSettings = new GameSettings
        {
            musicVolume = 1.0f,
            fxVolume = 1.0f,
            controller = "keyboard"
        };

        gameState = new SerializableDictionary<string, string>();

        //TODO: find a way to abstract this for multiple types
        PropertyInfo[] gameStateProps = typeof(GameStateDefault).GetProperties();
        foreach (PropertyInfo property in gameStateProps)
        {
            string propertyName = property.Name;
            string propertyValue = property.GetValue(gameStateDefault).ToString();
            gameState.Add(propertyName, propertyValue);
            Debug.Log($"property: {propertyName} {property.GetValue(gameStateDefault)}");
        }
    }
}
