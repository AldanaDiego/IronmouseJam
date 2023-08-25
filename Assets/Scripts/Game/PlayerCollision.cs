using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event EventHandler OnPlayerCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            OnPlayerCollision?.Invoke(this, EventArgs.Empty);
        }
    }
}
