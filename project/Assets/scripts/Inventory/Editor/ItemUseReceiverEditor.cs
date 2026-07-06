using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ItemUseReceiver))]
public class ItemUseReceiverEditor : Editor
{
    private SerializedProperty requiredItemsProperty;
    private string[] itemDisplayNames;
    private string[] itemIds;
    private GlobalGameData.InventoryItem[] inventoryItems;

    private void OnEnable()
    {
        requiredItemsProperty = serializedObject.FindProperty("requiredItems");
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
        EditorGUILayout.LabelField("Required Items", EditorStyles.boldLabel);

        // Show the list with custom dropdown
        if (itemDisplayNames != null && itemDisplayNames.Length > 0)
        {
            int arraySize = requiredItemsProperty.arraySize;
            
            EditorGUILayout.BeginVertical();
            
            for (int i = 0; i < arraySize; i++)
            {
                SerializedProperty element = requiredItemsProperty.GetArrayElementAtIndex(i);
                string currentId = element.stringValue;
                
                EditorGUILayout.BeginHorizontal();
                
                // Find current index by ID
                int currentIndex = System.Array.IndexOf(itemIds, currentId);
                if (currentIndex < 0) currentIndex = 0;
                
                // Dropdown showing names, storing IDs
                int newIndex = EditorGUILayout.Popup(currentIndex, itemDisplayNames);
                
                if (newIndex != currentIndex)
                {
                    element.stringValue = itemIds[newIndex];
                }
                
                // Remove button
                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    requiredItemsProperty.DeleteArrayElementAtIndex(i);
                    break;
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndVertical();
            
            // Add button
            if (GUILayout.Button("Add Required Item"))
            {
                requiredItemsProperty.arraySize++;
                SerializedProperty newElement = requiredItemsProperty.GetArrayElementAtIndex(arraySize);
                newElement.stringValue = itemIds[0];
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No inventory items found in GlobalGameData. Add items to the inventory in GlobalGameData.cs first.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
