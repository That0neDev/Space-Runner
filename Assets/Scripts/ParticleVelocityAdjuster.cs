using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleVelocityAdjuster : MonoBehaviour
{
    public GlobalValues values;
    public ParticleSystem particles;
    public float spawnSpeed;
    public float min,max;

    private void Update()
    {
        var main = particles.main;
        var rate = particles.emission;

        main.startSpeed = values.current;
        main.startSize = new ParticleSystem.MinMaxCurve(min * values.current, max * values.current);
        rate.rateOverTime = spawnSpeed * 5 * values.current;
    }
}
