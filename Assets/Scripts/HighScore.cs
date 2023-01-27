using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScore : MonoBehaviour
{
    public static HighScore Instance;
    public TextMeshProUGUI highScoreText;
    public string userName;
    public string highScoreName;
    public int score;
    public int highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        highScoreText.text = "Best Score : " + highScoreName +": "+ highScore;

        
    }

    public void ReadStringInput(string s)
    {
        userName = s;
    }

    public void SaveHighScore()
    {
        SaveData saveData = new SaveData();
        saveData.highScoreName = highScoreName;
        saveData.highScore = highScore;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            highScore = saveData.highScore;
            highScoreName = saveData.highScoreName;
        }
    }

    [Serializable]
    public class SaveData
    {
        public string highScoreName;
        public int highScore;
    }
}

