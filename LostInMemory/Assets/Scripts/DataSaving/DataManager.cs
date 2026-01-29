using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData playerData = new PlayerData();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveFromJson()
    {
        string data = JsonUtility.ToJson(playerData);
        string filePath = Application.persistentDataPath + "/playerData.json";
        Debug.Log(filePath);
        File.WriteAllText(filePath, data);
        Debug.Log("Data Saved");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";

        if (CheckExists(filePath))
        {
            string data = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log("Data Loaded");
        }
        else
        {
            Debug.LogWarning("No save file found at " + filePath);
        }
    }

    private bool CheckExists(string filePath)
    {
        return File.Exists(filePath);
    }

    public void DeleteFromJson()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";
        if(CheckExists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Data Deleted");
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public float[] position;
}