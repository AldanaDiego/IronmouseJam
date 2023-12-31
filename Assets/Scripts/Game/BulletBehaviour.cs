using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private const float ROTATION_SPEED = -180f;
    private float MovementSpeed;

    private Transform _transform;
    private SFXManager _sfxManager;
    private float _horizontalBound;
    private bool _isActive;

    private void Start()
    {
        _sfxManager = SFXManager.GetInstance();
        Vector3 bounds = GameBounds.GetInstance().GetScreenBounds();
        _horizontalBound = bounds.x + 3f;
        _transform = transform;
        MovementSpeed = DifficultySettings.GetInstance().GetBulletSpeed();
        _isActive = true;    
    }

    private void Update()
    {
        if (_isActive)
        {
            _transform.position += Vector3.right * (Time.deltaTime * MovementSpeed);
            if (_transform.position.x >= _horizontalBound)
            {
                Destroy(gameObject);
            }
            _transform.Rotate(Vector3.forward, ROTATION_SPEED * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _sfxManager.PlayBulletHit();
            Destroy(gameObject);
        }    
    }
}
