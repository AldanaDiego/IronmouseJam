using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Renderer _renderer;

    private float MovementSpeed;
    private Transform _transform;
    private SFXManager _sfxManager;
    private bool _isMoving = false;
    private float _leftBound;
    private bool _hasCollided;

    private void Start()
    {
        _hasCollided = false;
        _sfxManager = SFXManager.GetInstance();
        MovementSpeed = DifficultySettings.GetInstance().GetRockObstacleSpeed();  
    }

    private void Update()
    {
        if (_isMoving)
        {
            _transform.position += Vector3.left * (MovementSpeed * Time.deltaTime);
            if (_transform.position.x <= _leftBound)
            {
                Destroy(gameObject);
            }
        }    
    }

    public void Setup(float leftBound)
    {
        _transform = transform;
        _leftBound = leftBound;
        _hasCollided = false;
        _isMoving = true;
    }

    public bool CanCollide()
    {
        return !_hasCollided;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasCollided && (other.tag == "Player" || other.tag == "Board" || other.tag == "Enemy"))
        {
            _hasCollided = true;
            _sfxManager.PlayObstacleHit();
            _explosionEffect.Play();
            _renderer.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
