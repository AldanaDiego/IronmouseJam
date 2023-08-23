using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 8f;
    
    private InputManager _inputManager;
    private Transform _transform;
    private bool _canMove = true;
    private float _movementBoundHorizontal;
    private float _movementBoundVertical;

    private void Start()
    {
        _transform = transform;
        _inputManager = InputManager.GetInstance();
        Vector2 bounds = GameBounds.GetInstance().GetScreenBounds();
        _movementBoundHorizontal = bounds.x * 0.6f;
        _movementBoundVertical = bounds.y;
    }

    private void Update()
    {
        if (_canMove)
        {
            Vector2 movement = _inputManager.GetPlayerMovement();
            if (movement != Vector2.zero)
            {
                movement = Vector2.ClampMagnitude(movement, 1f) * (MOVEMENT_SPEED * Time.deltaTime);
                _transform.position = new Vector3(
                    Mathf.Clamp(_transform.position.x + movement.x, _movementBoundHorizontal * -1, _movementBoundHorizontal),
                    0f,
                    Mathf.Clamp(_transform.position.z + movement.y, _movementBoundVertical * -1, _movementBoundVertical)
                );
            }
        }    
    }

}
