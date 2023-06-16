using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] SoundArray sounds;
    [SerializeField] SoundPlayer soundPlayer;
    [SerializeField] float timeBeginning;
    [SerializeField] float minSilentTimeBetweenSongs,maxSilentTimeBetweenSongs;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeBeginning);
        int music = Random.Range(0,sounds.clips.Count);
        AudioClip clip = sounds.clips[music];
        int seconds = (int)clip.length;
        while(true){
            soundPlayer.StartPlayingRandom();
            float extraTime = Random.Range(maxSilentTimeBetweenSongs,maxSilentTimeBetweenSongs);
            yield return new WaitForSeconds(seconds + extraTime);
        }
    }
}
