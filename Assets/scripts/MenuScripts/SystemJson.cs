using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SystemJson : MonoBehaviour
{
    private int _userScore;
    private static SystemJson _instance;
    public static SystemJson Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogWarning("Singelton null");
            }
            return _instance;
        }
    }
    private int[] _highScore = new int[6];
    public int[] HighScore
    {
        get
        {
            return _highScore;
        }
        set
        {
            _highScore = value;
            Save();
        }
    }
    private void SelectionSort(int[] unsortedList)
    {
        int min;
        int tempSwappingSpace;
        for (int i = 0; i < unsortedList.Length; i++)
        {
            min = i;
            for (int j = i; j < unsortedList.Length; j++)
            {
                min = j;

            }
            if (min != i)
            {
                tempSwappingSpace = unsortedList[i];
                unsortedList[i] = unsortedList[min];
                unsortedList[min] = tempSwappingSpace;
            }
        }
    }
    private void Load()
    {
        if (File.Exists(Application.dataPath + "/highscores.txt"))
        {
            string loadString = File.ReadAllText(Application.dataPath + "/highscores.txt");
            SaveJson loadedSaveObject = JsonUtility.FromJson<SaveJson>(loadString);
            _highScore = loadedSaveObject.bestScores;
        }
        /*
        else
        {
            //Debug.LogWarning("Test");
            for (int i = 0; i < _highScore.Length; i++)
            {
                _highScore[i] = 999;
            }
        }
        */
    }
    public void Save() 
    {
        SaveJson saveObject = new SaveJson { bestScores = _highScore };
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/highscores.txt", json);
        _userScore = Menu.score;
        _highScore[0] = _userScore;
    }
    private void Awake()
    {
        
        _instance = this;

        Load();
    }
    /*
    private void Update() //do usuniecia potem
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            _highScore[0] = 420;
            _highScore[5] = 260;
            Save(); 
        }
    }
    */
}
