using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRockMovement : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 3.5f;
    private bool _isMoving = false;
    private float _leftBound;
    private float _respawnPosition;
    private Transform _transform;

    private void Update()
    {
        if (_isMoving)
        {
            _transform.position += Vector3.left *( MOVEMENT_SPEED * Time.deltaTime);
            if (_transform.position.x <= _leftBound)
            {
                Respawn();
            }
        }    
    }

    public void Setup(float position, float leftBound, float respawnPosition)
    {
        _transform = transform;
        _transform.position = new Vector3(position, Random.Range(0.1f, 0.5f) * -1, _transform.position.z);
        _transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        _leftBound = leftBound;
        _respawnPosition = respawnPosition;
        _isMoving = true;
    }

    public void Respawn()
    {
        _transform.position = new Vector3(_respawnPosition, Random.Range(0.1f, 0.5f) * -1, _transform.position.z);
        _transform.Rotate(Vector3.up, Random.Range(0f, 360f));
    }
}
