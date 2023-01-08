using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveSystemInformation (SaveData saveData, string FileName)
    {
        // Saves data in binary
        BinaryFormatter formatter = new BinaryFormatter();

        // Filepath to store data
        string path = Application.persistentDataPath + "/" + FileName +  ".test";

        //Debug.Log(path);

        // Filestream
        FileStream stream = new FileStream(path, FileMode.Create);

        // Data to save
        SystemSaveData systemdata = new SystemSaveData(saveData);

        // Serialize data
        formatter.Serialize(stream, systemdata);
        stream.Close();

    }

    public static SystemSaveData LoadData (string FileName)
    {
        string path = Application.persistentDataPath + "/" + FileName + ".test";

        // If save file exists
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // Reading binary file
            SystemSaveData systemdata =  formatter.Deserialize(stream) as SystemSaveData;
            stream.Close();

            return systemdata;
        }
        else
        {
            //Debug.LogError("Save file not found : " + path);
            return null;
        }
    }
}
