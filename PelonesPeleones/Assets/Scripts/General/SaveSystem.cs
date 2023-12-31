﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem 
{
    public static void SaveGame(GameFlow gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/gamedata.lid";
        FileStream stream = new FileStream(path,FileMode.Create);

        GameData data = new GameData(gameData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/gamedata.lid";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
