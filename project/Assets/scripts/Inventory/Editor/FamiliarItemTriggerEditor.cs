using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Yarn.Unity;

[CustomEditor(typeof(FamiliarItemTrigger))]
public class FamiliarItemTriggerEditor : Editor
{
    private SerializedProperty familiarIdProperty;
    private SerializedProperty completedDialogProperty;
    private string[] familiarDisplayNames;
    private string[] familiarIds;
    private GlobalGameData.Familiar[] familiars;
    private string[] dialogNodeNames;

    private void OnEnable()
    {
        familiarIdProperty = serializedObject.FindProperty("familiarId");
        completedDialogProperty = serializedObject.FindProperty("completedDialog");
        LoadFamiliars();
        LoadDialogNodes();
    }

    private void LoadFamiliars()
    {
        // Create an instance of GlobalGameData to access the default inventory structure
        GlobalGameData gameData = new GlobalGameData();
        
        if (gameData.inventory != null && gameData.inventory.familiars != null)
        {
            familiars = gameData.inventory.familiars.ToArray();
            
            // Extract display names and IDs for the dropdown
            familiarDisplayNames = familiars
                .Select(familiar => familiar.name)
                .ToArray();
            
            familiarIds = familiars
                .Select(familiar => familiar.id)
                .ToArray();
        }
        else
        {
            familiars = new GlobalGameData.Familiar[0];
            familiarDisplayNames = new string[0];
            familiarIds = new string[0];
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
        EditorGUILayout.LabelField("Familiar Selection", EditorStyles.boldLabel);

        // Show the dropdown for familiarId
        if (familiarDisplayNames != null && familiarDisplayNames.Length > 0)
        {
            string currentId = familiarIdProperty.stringValue;
            
            // Find current index by ID
            int currentIndex = System.Array.IndexOf(familiarIds, currentId);
            if (currentIndex < 0) currentIndex = 0;
            
            // Dropdown showing names, storing IDs
            int newIndex = EditorGUILayout.Popup("Familiar Name", currentIndex, familiarDisplayNames);
            
            if (newIndex != currentIndex)
            {
                familiarIdProperty.stringValue = familiarIds[newIndex];
            }
            
            // Show the current ID for reference
            EditorGUILayout.LabelField("Selected ID:", familiarIdProperty.stringValue);
        }
        else
        {
            EditorGUILayout.HelpBox("No familiars found in GlobalGameData. Add familiars to the inventory in GlobalGameData.cs first.", MessageType.Warning);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dialog Selection", EditorStyles.boldLabel);

        // Show the dropdown for completedDialog
        if (dialogNodeNames != null && dialogNodeNames.Length > 0)
        {
            string currentDialog = completedDialogProperty.stringValue;

            // Find current index
            int currentDialogIndex = System.Array.IndexOf(dialogNodeNames, currentDialog);
            if (currentDialogIndex < 0) currentDialogIndex = 0;

            // Dropdown showing dialog node names
            int newDialogIndex = EditorGUILayout.Popup("Familiar Dialog", currentDialogIndex, dialogNodeNames);

            if (newDialogIndex != currentDialogIndex)
            {
                completedDialogProperty.stringValue = dialogNodeNames[newDialogIndex];
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No dialog nodes found in MainDialogProject.yarnproject. Make sure the project exists and has nodes.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
