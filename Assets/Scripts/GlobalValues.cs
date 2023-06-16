using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalValues : MonoBehaviour
{
    [SerializeField] Transform obstacleParent;
    [SerializeField] ObstacleSpawner spawner;
    [SerializeField] List<float> multipliers;
    [SerializeField] GameObject destroyBoom,player;
    [SerializeField] SoundPlayer boomPlayer,musicPlayer;
    public CanvasGroup menu,gameOver,scoreCanvas;
    public ScoreCounter score;
    public HighScoreContainer scoreContainer;
    public bool active;
    public bool canStartGame;

    public float current;

    public int index = 0;

    public void IncreaseLevel(){
        if (multipliers.Count == index + 1)
            return;

        index += 1;
        current = multipliers[index];
    }

    public void StartGame(){

        if(canStartGame == false)
            return;
        
        StartCoroutine(SetFrame(scoreCanvas,1,0.5f));
        StartCoroutine(SetFrame(menu,0,0.5f));
        canStartGame = false;
        index = 0;
        IncreaseLevel();
        active = true;

       StartCoroutine(Level());
    }

    private IEnumerator Level(){
        float timePerLevel = 3;
        float increment = 2f;
        float waited = 0;

        while(active){
            float timeToWait = timePerLevel + (current * increment);
            waited += Time.deltaTime;

            if(waited > timeToWait){
                IncreaseLevel();
                waited = 0;
            }

            yield return Time.deltaTime;
        }
    }

    private IEnumerator ChangeFrame(CanvasGroup newFrame,CanvasGroup oldFrame){
        float totalTime = 1f;
        float delta = 0;

        while (delta < totalTime)
        {   
            delta += Time.deltaTime;
            newFrame.alpha = delta / totalTime;
            oldFrame.alpha = 1 - delta / totalTime;
            yield return Time.deltaTime;
        }

        newFrame.alpha = 1;
        oldFrame.alpha = 0;
        newFrame.interactable = true;
        newFrame.blocksRaycasts = true;
        oldFrame.interactable = false;
        oldFrame.blocksRaycasts = false;
    }

    private IEnumerator SetFrame(CanvasGroup group,float target,float time){
        float delta = 0;
        float start = group.alpha;

        while (delta < time)
        {
            delta += Time.deltaTime;
            group.alpha = Mathf.Lerp(start,target,delta/time);
            yield return Time.deltaTime;
        }

        group.alpha = target;

        if(target > 0){
            group.interactable = true;
            group.blocksRaycasts = true;
        }
        else{
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }

    private IEnumerator WaitForObstacles(){

        yield return new WaitForSeconds(1f);
        for (int i = obstacleParent.childCount - 1; i >= 0; i--)
        {
            Destroy(obstacleParent.GetChild(i).gameObject);
        }


        StartCoroutine(ChangeFrame(menu,gameOver)); //1 sec
        yield return new WaitForSeconds(1f);

        canStartGame = true;
    }

    public IEnumerator EndGame(){
        scoreContainer.UpdateScore(score.Score);
        index = 0;
        current = multipliers[0];
        score.Score = 0;
        active = false;
        
        Instantiate(destroyBoom,player.transform.position,Quaternion.identity);
        boomPlayer.StartPlayingRandom();
        player.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        player.transform.GetChild(0).gameObject.SetActive(false);
        Invoke("ShowPlane",3f);
        
        StartCoroutine(SetFrame(scoreCanvas,0,0.5f));
        StartCoroutine(SetFrame(gameOver,1,1.25f));
        yield return new WaitForSeconds(1.25f);

        StartCoroutine(WaitForObstacles());
    }

    private void ShowPlane(){
        player.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        player.transform.GetChild(0).gameObject.SetActive(true);
    }

    private IEnumerator Start(){
        Application.targetFrameRate = 60;
        scoreContainer.UpdateScore(0);
        StartCoroutine(SetFrame(menu,1,1f));
        yield return new WaitForSeconds(1f);

        canStartGame = true;
    }
}
