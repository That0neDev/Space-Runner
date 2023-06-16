using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpeed : MonoBehaviour
{
    public Material mat;
    public GlobalValues values;
    public float current;
    public float speed;

    private void Update(){
        current += Time.deltaTime * values.current * speed;
        mat.mainTextureOffset = new Vector2(0,current);
    }
}
