using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float defaultSpawnSpeed;
    public float defaultObstacleSpeed;
    public float currentMultiplier,current,beginTime;
    public GlobalValues values;

    private bool spawning;

    public GameObject prefab;
    public Transform parent;

    private IEnumerator OnActive()
    {
        yield return new WaitForSeconds(beginTime);
        float speedMultiplier = 0.8f;

        while(values.active){
            yield return new WaitForSeconds(current * speedMultiplier);

            SpawnObstacle(-1);
        }

        spawning = false;
    }

    private void Update(){
        if(values.active){
            currentMultiplier = values.current;
            current = defaultSpawnSpeed / currentMultiplier;

            if(spawning == false){
                spawning = true;
                StartCoroutine(OnActive());
            }
        }
    }

    private void SpawnObstacle(int first){
        float startSize = 4.5f;
        float reduceMultiplier = 0.04f;

        Vector2[] positions = new Vector2[]
        {
            new Vector2(-3,20),
            new Vector2 (0,20),
            new Vector2 (3,20)
        };
        
        float doubleChance = 0.2f;
        
        int GetNumber(){
            int r = Random.Range(0,3);
            if(first == r)
                return GetNumber();
                
            return r;
        }
        int number = GetNumber();
        GameObject newObject = Instantiate(prefab,positions[number],Quaternion.identity);
        newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-1 * defaultObstacleSpeed * currentMultiplier);
        newObject.transform.parent = parent;
        float size = startSize * (1 - reduceMultiplier * currentMultiplier);
        newObject.transform.localScale = new Vector3(size,size,1);

        if(first == -1){
            bool isDouble = Random.Range(0,1f) < doubleChance;
            if(isDouble)
                SpawnObstacle(number);
        }
    }
}
