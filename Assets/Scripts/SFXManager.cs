using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonClicked;
    [SerializeField] private AudioClip _bulletShot;
    [SerializeField] private AudioClip _bulletHit;
    [SerializeField] private AudioClip _obstacleHit;
    [SerializeField] private AudioClip _jumpStart;
    [SerializeField] private AudioClip _jumpLanding;
    [SerializeField] private AudioClip _victory;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void PlayButtonClicked()
    {
        _audioSource.PlayOneShot(_buttonClicked);
    }

    public void PlayBulletShot()
    {
        _audioSource.PlayOneShot(_bulletShot);
    }

    public void PlayBulletHit()
    {
        _audioSource.PlayOneShot(_bulletHit);
    }

    public void PlayObstacleHit()
    {
        _audioSource.PlayOneShot(_obstacleHit, 0.5f);
    }

    public void PlayJumpStart()
    {
        _audioSource.PlayOneShot(_jumpStart);
    }

    public void PlayJumpLanding()
    {
        _audioSource.PlayOneShot(_jumpLanding);
    }

    public void PlayVictory()
    {
        _audioSource.PlayOneShot(_victory);
    }
}
