using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(FamiliarUseReceiver))]
public class FamiliarUseReceiverEditor : Editor
{
    private SerializedProperty requiredFamiliarProperty;
    private string[] familiarDisplayNames;
    private string[] familiarIds;
    private GlobalGameData.Familiar[] familiars;

    private void OnEnable()
    {
        requiredFamiliarProperty = serializedObject.FindProperty("requiredFamiliar");
        LoadFamiliars();
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

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Required Familiar", EditorStyles.boldLabel);

        // Show the dropdown for requiredFamiliar
        if (familiarDisplayNames != null && familiarDisplayNames.Length > 0)
        {
            string currentId = requiredFamiliarProperty.stringValue;
            
            // Find current index by ID
            int currentIndex = System.Array.IndexOf(familiarIds, currentId);
            if (currentIndex < 0) currentIndex = 0;
            
            // Dropdown showing names, storing IDs
            int newIndex = EditorGUILayout.Popup("Familiar Name", currentIndex, familiarDisplayNames);
            
            if (newIndex != currentIndex)
            {
                requiredFamiliarProperty.stringValue = familiarIds[newIndex];
            }
            
            // Show the current ID for reference
            EditorGUILayout.LabelField("Selected ID:", requiredFamiliarProperty.stringValue);
        }
        else
        {
            EditorGUILayout.HelpBox("No familiars found in GlobalGameData. Add familiars to the inventory in GlobalGameData.cs first.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
