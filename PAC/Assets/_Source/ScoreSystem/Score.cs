using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    private int bestscore;
    void Save()
    {
        if(score > bestscore)
        {
            PlayerPrefs.SetInt("bestscore", score);
        }
    }
    void Load()
    {
        bestscore = PlayerPrefs.GetInt("bestscore");
    }
}
