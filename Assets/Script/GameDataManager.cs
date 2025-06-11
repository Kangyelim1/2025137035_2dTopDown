using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData
{
    public List<string> collectsdItems = new List<string>();

    public int stage = 1;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Intance;

    public PlayerData playerData;

    private void Awake()
    {
        if (Intance == null)
        {
            Intance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(PlayerData playerData)
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        string json = JsonUtility.ToJson(playerData, true);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("게임 데이터 저장됨:" + json);
    }

    public PlayerData LoadData()
    {
        string filepath = Application.persistentDataPath + "/player_data.json";
        if (System.IO.File.Exists(filepath))
        {
            string json = System.IO.File.ReadAllText(filepath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("게임 데이터 로드됨:" + json);
            return playerData;
        }
        else
        {
            Debug.LogWarning("저장된 게임 데이터가 없습니다.");
            return new PlayerData();
        }
    }

    public void GameStart()
    {
        playerData = LoadData();
      if (playerData == null)
      {
            playerData = new PlayerData();
            SceneManager.LoadScene("Level_1");
      }
      else
      {
            SceneManager.LoadScene("Level_1" + playerData);
      }
    }

    public void PlayerDead()
    {
        PlayerData playerData = LoadData();
        if(playerData != null)
        {
            playerData.stage = 1;

            foreach (string item in playerData.collectsdItems.ToList())
            {
                if (UnityEngine.Random.Range(0, 1) == 0)
                {
                    playerData.collectsdItems.Remove(item);
                }
            }
            SaveData(playerData);
        }
        SceneManager.LoadScene("GameOver");
    }

    
}
