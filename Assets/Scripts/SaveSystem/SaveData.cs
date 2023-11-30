using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct AIData
    {
        public Vector3 pos;
        public int m_Health;
    }

    [System.Serializable]
    public struct PlayerData
    {
        public Vector3 pos;
    }

    public Random.State state;
    public int m_Score;
    public AIData m_AIData = new AIData();
    public PlayerData m_PlayerData = new PlayerData();

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}