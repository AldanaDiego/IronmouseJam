using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 1f;

    private Transform _transform;
    private float _rightEdge;
    private float _leftEdge;
    private Vector3 _edgeDistance;
    private bool _isMoving;

    private void Start()
    {
        _transform = transform;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        _rightEdge = _transform.position.x + sprite.bounds.extents.x / 3f;
        _leftEdge = _transform.position.x - sprite.bounds.extents.x / 3f;
        _edgeDistance = new Vector3(_rightEdge - _leftEdge, 0f, 0f);
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _transform.position += Vector3.left * (_scrollSpeed * Time.deltaTime);
            if (_transform.position.x < _leftEdge)
            {
                _transform.position += _edgeDistance;
            }
        }
    }
}
