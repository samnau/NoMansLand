using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Yarn.Unity;

[CustomEditor(typeof(InventoryItemTrigger))]
public class InventoryItemTriggerEditor : Editor
{
    private SerializedProperty itemIdProperty;
    private SerializedProperty collectedDialogProperty;
    private string[] itemDisplayNames;
    private string[] itemIds;
    private GlobalGameData.InventoryItem[] inventoryItems;
    private string[] dialogNodeNames;

    private void OnEnable()
    {
        itemIdProperty = serializedObject.FindProperty("itemId");
        collectedDialogProperty = serializedObject.FindProperty("collectedDialog");
        LoadInventoryItems();
        LoadDialogNodes();
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

    private void LoadDialogNodes()
    {
        // Find all .yarn files in the project
        var sourceScripts = new List<string>();
        var yarnFiles = AssetDatabase.FindAssets("t:TextAsset", new[] { "Assets" });

        foreach (var guid in yarnFiles)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.EndsWith(".yarn"))
            {
                var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                if (textAsset != null)
                {
                    var nodeNames = ExtractNodeNamesFromYarnScript(textAsset.text);
                    sourceScripts.AddRange(nodeNames);
                }
            }
        }

        dialogNodeNames = sourceScripts.Distinct().ToArray();
    }

    private List<string> ExtractNodeNamesFromYarnScript(string yarnText)
    {
        var nodeNames = new List<string>();
        var lines = yarnText.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            var trimmedLine = lines[i].Trim();

            // YarnSpinner 2.2 format: title: NodeName on its own line
            if (trimmedLine.StartsWith("title:"))
            {
                var titlePart = trimmedLine.Substring(6).Trim();
                if (!string.IsNullOrEmpty(titlePart))
                {
                    nodeNames.Add(titlePart);
                }
            }
        }

        return nodeNames;
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

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dialog Selection", EditorStyles.boldLabel);

        // Show the dropdown for collectedDialog
        if (dialogNodeNames != null && dialogNodeNames.Length > 0)
        {
            string currentDialog = collectedDialogProperty.stringValue;

            // Find current index
            int currentDialogIndex = System.Array.IndexOf(dialogNodeNames, currentDialog);
            if (currentDialogIndex < 0) currentDialogIndex = 0;

            // Dropdown showing dialog node names
            int newDialogIndex = EditorGUILayout.Popup("Collected Dialog", currentDialogIndex, dialogNodeNames);

            if (newDialogIndex != currentDialogIndex)
            {
                collectedDialogProperty.stringValue = dialogNodeNames[newDialogIndex];
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No dialog nodes found in MainDialogProject.yarnproject. Make sure the project exists and has nodes.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
