using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BeatManager : MonoBehaviour
{
    public List<Sequence> sequenceList;
    public float stepTime;
    public float initTimer;
    public Audio[] audioList;

    private float timer;
    private AudioSource[] audioSources;
    private int index = 0;

    private void Start()
    {
        audioSources = new AudioSource[audioList.Length];
        timer = initTimer;
        int i = 0;
        foreach (var soundclip in audioList)
        {
            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.clip = soundclip.clip;
            s.volume = soundclip.volume;
            s.playOnAwake = false;
            audioSources[i] = s;
            i++;
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            foreach (var sequence in sequenceList)
            {
                int a = (int)sequence.sequence[index % (sequence.sequence.Length)] - 1;
                if (a >= 0)
                {
                    audioSources[a].Play();
                    Debug.Log("Played" + (Sequence.Beat)a);
                }
            }
            index++;
            timer = stepTime;
        }
    }
}
