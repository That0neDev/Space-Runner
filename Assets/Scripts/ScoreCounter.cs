using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private bool counting;
    public GlobalValues values;
    public Text text;
    public int Score;

    private IEnumerator Count(){
        while (counting && values.active)
        {
            Score += Mathf.CeilToInt(values.current);
            text.text = Score.ToString();

            yield return new WaitForSeconds(1);
        }
        counting = false;
    }

    private void Update(){
        if(values.active && counting == false){
            counting = true;
            StartCoroutine(Count());
        }
    }
}
