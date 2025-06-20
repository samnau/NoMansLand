using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class GlobalGameData
{
    // This file is the Schema for your save data structure
    // Use it as the source of truth for all game data
    //public int dayCount;
    //public int deathCount;

    //public SerializableDictionary<string, string> gameState;
    //public class GameStateDefault
    //{
    //    public bool gameComplete { get; set; }
    //    public bool gameInProgress { get; set; }
    //    public string currentScene { get; set; }
    //    public string lastDirection { get; set; }
    //    public int dayCount { get; set; }
    //}

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

    [System.Serializable]
    public class OneTimeEvents
    {
        public bool castleCourtyard;
        public bool castleThroneRoom;
    }

    [System.Serializable]
    public class InventoryItem
    {
        public string id;
        public string name;
        public string description;
        public bool active;
        public bool collected;
        public string imageId;
    }

    [System.Serializable]
    public class Familiar : InventoryItem
    {
        public string weakness;
    }

    [System.Serializable]
    public class Inventory
    {
        public List<InventoryItem> items;
        public List<Familiar> familiars;
    }

    [System.Serializable]
    public class EnvironmentState
    {
        public bool castleWalls;
        public bool castleFloatingStairs;
        public bool castleCourtYardRightDoor;
        public bool forestBridge;
    }

    // TODO: working list of real image assets listed by a key string
    // NOTE: maybe only house the image dictionary within the inventory manager code?
    // public Dictionary<string, Image> itemImageLibrary

    // public GameStateDefault gameStateDefault;
    public GameSettings gameSettings;
    public WorldState worldState;
    public EnvironmentState environment;
    public OneTimeEvents oneTimeEvents;
    public Inventory inventory;
    public List<InventoryItem> test;

    
    // Values in the constructor below will be the default values for any new instance of the save data
    public GlobalGameData()
    {
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

        oneTimeEvents = new OneTimeEvents
        {
            castleCourtyard = false,
            castleThroneRoom = false
        };

        test = new List<InventoryItem>();

        //test.Add(new InventoryItem
        //{
        //    id = "castleStairsGateKey",
        //    name = "Castle Stairs Gate Key",
        //    description = "A key that unlocks the gate to the floating stairs in the castle.",
        //    active = false,
        //    collected = false
        //});

        inventory = new Inventory
        {
            items = new List<InventoryItem>(),
            familiars = new List<Familiar>()
        };

        inventory.items.Add(new InventoryItem
        {
            id = "castleStairsGateKey",
            name = "Castle Stairs Gate Key",
            description = "A key that unlocks the gate to the floating stairs in the castle.",
            active = false,
            collected = false
        });

        inventory.familiars.Add(new Familiar
        {
            id = "defaultFamiliar",
            name = "Froggy",
            description = "A small frog you found by a pool in the forest. Magical apparently. Not sure how.",
            active = false,
            collected = false,
            weakness = "none"
        });
    }
}