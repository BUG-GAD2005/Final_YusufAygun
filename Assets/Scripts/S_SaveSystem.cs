using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class S_SaveSystem
{
    
    public static void SaveGame(int Gold, int Gem, CellStruct[,] TheGrid)
    {
        BinaryFormatter formatter= new BinaryFormatter();

        string path = Application.persistentDataPath + "/GameInstance";

        FileStream stream = new FileStream(path, FileMode.Create);

        S_GameData data = new S_GameData( Gold,  Gem, TheGrid); // bu kýsmý deðiþtircen


        formatter.Serialize(stream, data);

        stream.Close();
    }


    public static S_GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/GameInstance";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter(); 

            FileStream stream = new FileStream(path, FileMode.Open);

            S_GameData data = formatter.Deserialize(stream) as S_GameData;

            stream.Close();
            
            return data;
        }
        else
        {
            Debug.Log("NoSaveFound " + path);
            return null;
        }
    }

}
