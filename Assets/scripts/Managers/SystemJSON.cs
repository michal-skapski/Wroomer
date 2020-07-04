using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SystemJSON : MonoBehaviour
{
    private static SystemJSON _instance;
    private int[] _highscore = new int[6];
    private string[] _bestPlayersNames = new string[6];
    private string _fileName = "/scores.txt";
    private void Awake()
    {
        _instance = this;
        Load();
    }
    public static SystemJSON Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("The system json is null!");
            }
            return _instance;
        }
    }
    public int[] Highscore
    {
        get
        {
            return _highscore;
        }
        set
        {
            _highscore = value;
            Save();
        }
    }
    public string[] BestPlayers
    {
        get
        {
            return _bestPlayersNames;
        }
        set
        {
            _bestPlayersNames = value;
            //Save();
        }
    }
    public void Save()
    {
        // save actually
        SaveJSON saveObject = new SaveJSON { bestScores = _highscore, bestPlayers = _bestPlayersNames };
        string json = JsonUtility.ToJson(saveObject);
        // save to a file
        File.WriteAllText(Application.dataPath + _fileName, json);
    }
    public void Load()
    {
        if (File.Exists(Application.dataPath + _fileName))
        {
            string loadString = File.ReadAllText(Application.dataPath + _fileName);
            SaveJSON loadedSaveObject = JsonUtility.FromJson<SaveJSON>(loadString);
            _highscore = loadedSaveObject.bestScores;
            _bestPlayersNames = loadedSaveObject.bestPlayers;
        }
        else
        {
            Debug.LogError("File with amount of scores do not exists!");
        }
    }
}