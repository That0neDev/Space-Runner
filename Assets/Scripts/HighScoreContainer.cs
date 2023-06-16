using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreContainer : MonoBehaviour
{
    public Text score;

    public int GetScore(){
        return PlayerPrefs.GetInt("Score",0);
    }

    public void UpdateScore(int newScore){
        if(newScore > PlayerPrefs.GetInt("Score",0))
            PlayerPrefs.SetInt("Score",newScore);
        
        string txt = GetScore().ToString();
        score.text = "Highscore: " + txt;
    }
}
