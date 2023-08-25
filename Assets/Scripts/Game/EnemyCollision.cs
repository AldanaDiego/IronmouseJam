using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public event EventHandler OnEnemyCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            OnEnemyCollision?.Invoke(this, EventArgs.Empty);
        }
    }
}
