using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public SoundArray sounds;
    public AudioSource audioSource;

    public void StartPlayingRandom(){
        AudioClip clip = sounds.clips[Random.Range(0,sounds.clips.Count)];
        Play(clip);
    }

    public void Play(AudioClip sound){
        audioSource.clip = sound;
        audioSource.Play();
    }
}
