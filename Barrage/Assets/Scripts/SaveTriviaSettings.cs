using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveTriviaSettings
{
    public static void SaveTSettings(TriviaSettings settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        TriviaData data = new TriviaData(settings);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static TriviaData LoadTSettings()
    {
        string path = Application.persistentDataPath + "/settings.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            TriviaData data = formatter.Deserialize(stream) as TriviaData;
            stream.Close();
            return data;
        } else
        {
            Debug.Log("save file not found at " + path);
            return null;
        }
    }

}
