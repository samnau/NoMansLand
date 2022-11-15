using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    string dataFilePath = "";
    string dataFileName = "";

    public FileDataHandler(string dataFilePath, string dataFileName)
    {
        this.dataFilePath = dataFilePath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataFilePath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception err)
            {
                Debug.LogError($"Error when trying to load file: {fullPath} - {err}");
            }
        }
        return loadedData;
    }

    public void Save (GameData data)
    {
        string fullPath = Path.Combine(dataFilePath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception err)
        {
            Debug.LogError($"Error when trying to save file: {fullPath} - {err}");
        }
    }
}
