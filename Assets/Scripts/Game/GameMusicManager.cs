using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    //TODO Testing music loop

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioStart;

    private const float LOOP_START_OFFSET = -0.15f;

    private void Start()
    {
        _audioSource.PlayOneShot(_audioStart);
        _audioSource.PlayScheduled(AudioSettings.dspTime + _audioStart.length + LOOP_START_OFFSET);    
    }
}
