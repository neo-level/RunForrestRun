using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> soundEffects;
    
    public void PlaySoundEffect(string clipToPlay)
    {
        switch (clipToPlay)
        {
            case "jump":
                audioSource.clip = soundEffects[0];
                break;
            case "crash":
                audioSource.clip = soundEffects[1];
                break;
            case "fire":
                audioSource.clip = soundEffects[2];
                break;
        }
        audioSource.Play();
    }
}
