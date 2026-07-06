using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(InventoryItemTrigger))]
public class InventoryItemTriggerEditor : Editor
{
    private SerializedProperty itemIdProperty;
    private string[] itemDisplayNames;
    private string[] itemIds;
    private GlobalGameData.InventoryItem[] inventoryItems;

    private void OnEnable()
    {
        itemIdProperty = serializedObject.FindProperty("itemId");
        LoadInventoryItems();
    }

    private void LoadInventoryItems()
    {
        // Create an instance of GlobalGameData to access the default inventory structure
        GlobalGameData gameData = new GlobalGameData();
        
        if (gameData.inventory != null && gameData.inventory.items != null)
        {
            inventoryItems = gameData.inventory.items.ToArray();
            
            // Extract display names and IDs for the dropdown
            itemDisplayNames = inventoryItems
                .Select(item => item.name)
                .ToArray();
            
            itemIds = inventoryItems
                .Select(item => item.id)
                .ToArray();
        }
        else
        {
            inventoryItems = new GlobalGameData.InventoryItem[0];
            itemDisplayNames = new string[0];
            itemIds = new string[0];
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Item Selection", EditorStyles.boldLabel);

        // Show the dropdown for itemId
        if (itemDisplayNames != null && itemDisplayNames.Length > 0)
        {
            string currentId = itemIdProperty.stringValue;
            
            // Find current index by ID
            int currentIndex = System.Array.IndexOf(itemIds, currentId);
            if (currentIndex < 0) currentIndex = 0;
            
            // Dropdown showing names, storing IDs
            int newIndex = EditorGUILayout.Popup("Item Name", currentIndex, itemDisplayNames);
            
            if (newIndex != currentIndex)
            {
                itemIdProperty.stringValue = itemIds[newIndex];
            }
            
            // Show the current ID for reference
            EditorGUILayout.LabelField("Selected ID:", itemIdProperty.stringValue);
        }
        else
        {
            EditorGUILayout.HelpBox("No inventory items found in GlobalGameData. Add items to the inventory in GlobalGameData.cs first.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
