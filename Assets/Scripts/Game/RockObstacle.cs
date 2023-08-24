using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 5f;
    private Transform _transform;
    private bool _isMoving = false;
    private float _leftBound;

    private void Update()
    {
        if (_isMoving)
        {
            _transform.position += Vector3.left * (MOVEMENT_SPEED * Time.deltaTime);
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
        _isMoving = true;
    }
}
