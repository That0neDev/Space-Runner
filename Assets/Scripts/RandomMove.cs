using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
        body.AddTorque(Random.Range(-10,10),ForceMode2D.Impulse);
    }
}
