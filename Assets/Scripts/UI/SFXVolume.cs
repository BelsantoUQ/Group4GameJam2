using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer SFXaudioMixer;

    public void ChangeVolume(float volume)
    {
        SFXaudioMixer.SetFloat("SFXVolume", volume);
    }
}
