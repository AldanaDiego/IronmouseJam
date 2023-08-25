using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 6.5f;
    private const float ROTATION_SPEED = -180f;

    private Transform _transform;
    private float _horizontalBound;
    private bool _isActive;

    private void Start()
    {
        Vector3 bounds = GameBounds.GetInstance().GetScreenBounds();
        _horizontalBound = bounds.x + 3f;
        _transform = transform;
        _isActive = true;    
    }

    private void Update()
    {
        if (_isActive)
        {
            _transform.position += Vector3.right * (Time.deltaTime * MOVEMENT_SPEED);
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
            //TODO sfx
            Destroy(gameObject);
        }    
    }
}
