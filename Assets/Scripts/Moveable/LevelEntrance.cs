using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEntrance : MonoBehaviour
{

    public event EventHandler OnTakePlayerToNextLevel;
    [SerializeField] private float speed;
    [SerializeField] private Transform upperPosition;

    private HashSet<GameObject> playersOnEntrance = new HashSet<GameObject>();

    private void LiftEntrance()
    {
        transform.position = Vector2.MoveTowards(transform.position,upperPosition.position,speed * Time.deltaTime);
    }

    private void Update() {
        if (playersOnEntrance.Count == 2)
        {
            OnTakePlayerToNextLevel?.Invoke(this,EventArgs.Empty);
            LiftEntrance();
            Debug.Log("Lift");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            playersOnEntrance.Add(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            if (playersOnEntrance.Contains(other.gameObject))
            {
                playersOnEntrance.Remove(other.gameObject);
            }
        }

    }
}
