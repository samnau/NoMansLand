using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GlobalFileDataHandler
{
    string dataDirPath = "";
    string dataFileName = "";

    public GlobalFileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GlobalGameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GlobalGameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GlobalGameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occured when trying to load data at {fullPath} \n {e}");
            }
        }

        return loadedData;
    }

    public void Save(GlobalGameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occured when trying to save data at {fullPath} \n {e}");
        }
    }
}
