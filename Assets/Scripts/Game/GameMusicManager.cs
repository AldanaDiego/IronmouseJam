using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    //This should be a dictionary but whatever
    [SerializeField] private AudioClip _normalMusic;
    [SerializeField] private AudioClip _hardMusic;
    [SerializeField] private AudioClip _ultimateMusicLoop;
    [SerializeField] private AudioClip _ultimateMusicStart;

    private const float LOOP_START_OFFSET = -0.15f;

    private void Start()
    {
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case (int) Consts.DIFFICULTIES.NORMAL:
                _audioSource.clip = _normalMusic;
                _audioSource.Play();
                break;

            case (int) Consts.DIFFICULTIES.HARD:
                _audioSource.clip = _hardMusic;
                _audioSource.Play();
                break;

            case (int) Consts.DIFFICULTIES.ULTIMATE:
                _audioSource.clip = _ultimateMusicLoop;
                _audioSource.PlayOneShot(_ultimateMusicStart);
                _audioSource.PlayScheduled(AudioSettings.dspTime + _ultimateMusicStart.length + LOOP_START_OFFSET);
                break;

            default:
                return;
        }  
    }
}
