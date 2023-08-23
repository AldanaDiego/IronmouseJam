using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRockSpawner : MonoBehaviour
{
    [SerializeField] List<BackgroundRockMovement> _rockPrefabs;

    private List<BackgroundRockMovement> _rocks;
    private Transform _transform;
    private const int ROCK_NUMBER = 16;
    private const float START_POSITION = -13f;
    private const float END_POSITION = 13f;

    private void Start()
    {
        _transform = transform;
        _rocks = new List<BackgroundRockMovement>();

        float spacing = (END_POSITION - START_POSITION) / ROCK_NUMBER;
        float currentPosition = START_POSITION;
        for (int i = 0; i < ROCK_NUMBER; i++)
        {
            BackgroundRockMovement rock = Instantiate(_rockPrefabs[Random.Range(0, _rockPrefabs.Count)], _transform);
            rock.Setup(currentPosition, START_POSITION, END_POSITION);
            currentPosition += spacing;
            _rocks.Add(rock);
        }
    }
}
