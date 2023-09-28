using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {
        source.clip = clip;
    }

    public void PlayMusic()
    {
        source.Play();
    }
}
