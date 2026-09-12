using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(InteractionTrigger))]
public class InteractionTriggerEditor : Editor
{
    private SerializedProperty targetTextProperty;
    private string[] dialogNodeNames;

    private void OnEnable()
    {
        targetTextProperty = serializedObject.FindProperty("targetText");
        LoadDialogNodes();
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

        dialogNodeNames = sourceScripts.Distinct().OrderBy(x => x).ToArray();
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
        EditorGUILayout.LabelField("Dialog Selection", EditorStyles.boldLabel);

        // Show the dropdown for targetText
        if (dialogNodeNames != null && dialogNodeNames.Length > 0)
        {
            string currentDialog = targetTextProperty.stringValue;

            // If empty, auto-assign the first dialog node
            if (string.IsNullOrEmpty(currentDialog))
            {
                currentDialog = dialogNodeNames[0];
                targetTextProperty.stringValue = currentDialog;
            }

            // Find current index
            int currentDialogIndex = System.Array.IndexOf(dialogNodeNames, currentDialog);
            if (currentDialogIndex < 0) currentDialogIndex = 0;

            // Dropdown showing dialog node names
            int newDialogIndex = EditorGUILayout.Popup("Target Dialog", currentDialogIndex, dialogNodeNames);

            if (newDialogIndex != currentDialogIndex)
            {
                targetTextProperty.stringValue = dialogNodeNames[newDialogIndex];
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No dialog nodes found in .yarn files. Make sure you have .yarn files with dialog nodes.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
