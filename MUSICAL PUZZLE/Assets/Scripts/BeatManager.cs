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
            bool[] playing = new bool[audioSources.Length];
            for (int i = 0; i < audioSources.Length; i++)
            {
                playing[i] = false;
            }

            foreach (var sequence in sequenceList)
            {
                if (sequence.sequence.Length == 0)
                    continue;

                for (int i = 0; i < audioSources.Length; i++)
                {
                    if (sequence.sequence[index % (sequence.sequence.Length)].HasFlag((Sequence.Beat)(i + 1)))
                    {
                        playing[i] = true;
                    }
                }
                /*
                int a = (int)sequence.sequence[index % (sequence.sequence.Length)] - 1;
                if (a >= 0)
                {
                    audioSources[a].Play();
                    Debug.Log("Played" + (Sequence.Beat)a);
                }
                */
            }

            for (int i = 0; i < audioSources.Length; i++)
            {
                if (playing[i])
                {
                    audioSources[i].Play();
                }
            }
            index++;
            timer = stepTime;
        }
    }
}
