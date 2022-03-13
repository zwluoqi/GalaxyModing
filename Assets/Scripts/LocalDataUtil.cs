using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalDataUtil
{
    public static void Save<T>(string key, T value, string fileName)
    {
        string text = Application.persistentDataPath + "/" + fileName;
        if (!Directory.Exists(text))
        {
            Directory.CreateDirectory(text);
        }
        string path = text + "/" + key;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(path);
        binaryFormatter.Serialize(fileStream, value);
        fileStream.Close();
    }

    public static T Load<T>(string key, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + "/" + key;
        if (File.Exists(path))
        {
            FileStream fileStream = null;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fileStream = File.Open(path, FileMode.Open);
                T result = (T)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return result;
            }
            catch (Exception ex)
            {
                fileStream?.Close();
                Debug.LogError("can not get save data, key=" + key + ", fileName=" + fileName);
                Debug.LogError(ex.StackTrace);
                return default(T);
            }
        }
        return default(T);
    }
}