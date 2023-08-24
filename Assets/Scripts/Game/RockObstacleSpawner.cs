using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacleSpawner : MonoBehaviour
{
    [SerializeField] private RockObstacle _rockPrefab;

    private float _upperBound;
    private float _lowerBound;
    private float _leftBound;
    private float _spawnPosition = 12f;
    private bool _isActive = false;
    private float _spawnCooldown = 3.5f; //TODO depends on difficulty
    private float _cooldownTimer = 0f;

    private void Start()
    {
        Vector2 bounds = GameBounds.GetInstance().GetScreenBounds();
        _upperBound = bounds.y;
        _lowerBound = bounds.y * -1;
        _leftBound = (bounds.x * -1) - 2f;
        _isActive = true;
    }

    private void Update()
    {
        if (_isActive)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _spawnCooldown)
            {
                RockObstacle rock = Instantiate(
                    _rockPrefab,
                    new Vector3(
                        _spawnPosition,
                        0f,
                        Mathf.RoundToInt(Random.Range(_lowerBound, _upperBound))
                    ),
                    Quaternion.identity
                );
                rock.Setup(_leftBound);
                _cooldownTimer = 0f;
            }
        }    
    }
}
