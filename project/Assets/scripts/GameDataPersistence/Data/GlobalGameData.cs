using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class GlobalGameData
{
    // This file is the Schema for your save data structure
    // Use it as the source of truth for all game data
    // NOTE: this file does not handle the save/load operations

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

        inventory = new Inventory
        {
            items = new List<InventoryItem> {
                new InventoryItem
                {
                    id = "castleStairsGateKey",
                    name = "Castle Stairs Gate Key",
                    description = "A key that unlocks the gate to the floating stairs in the castle.",
                    active = false,
                    collected = false
                },
                new InventoryItem
                {
                    id = "castleWallGem1",
                    name = "Mysterious Green Gem",
                    description = "A green gem found in the forest",
                    active = false,
                    collected = false
                },
                new InventoryItem
                {
                    id = "castleWallGem2",
                    name = "Mysterious Blue Gem",
                    description = "A blue gem found in the forest",
                    active =  false,
                    collected = false
                },
                new InventoryItem
                {
                    id = "bikeReflector",
                    name = "Bike Reflector",
                    description = "Just a regular red bike reflector",
                    active =  false,
                    collected = false
                },
                new InventoryItem
                {
                    id = "magicKey",
                    name = "My key",
                    description = "Just a fancy looking key I've always had. I guess it's a lot more than that?",
                    active =  true,
                    collected = false
                }
           },
            familiars = new List<Familiar>
            {
                new Familiar
                {
                    id = "defaultFamiliar",
                    name = "Froggy",
                    description = "A small frog you found by a pool in the forest. Magical apparently. Not sure how.",
                    active =  false,
                    collected =false,
                    weakness = "none"
                },
                new Familiar
                {
                    id = "forestFamiliar",
                    name = "To'leti",
                    description = "A crow. Or is it a raven? I can never tell the difference. Maybe it has wind powers?",
                    active =  false,
                    collected = false,
                    weakness = "defaultFamiliar",
                },
                new Familiar
                {
                    id = "caveFamiliar",
                    name = "Artrios",
                    description = "A bear. Pretty big. I think it can do earth magic.",
                    active =  false,
                    collected = false,
                    weakness = "forestFamiliar",
                }
            }
        };

    }
}