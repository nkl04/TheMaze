using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEntrance : MonoBehaviour
{
    public event EventHandler OnOutOfTheMap;
    public bool CanMove {get{return canMove;} set{canMove = value;}}
    [SerializeField] private float speed;
    [SerializeField] private Transform nextLevelPosition;

    private bool canMove;
    private HashSet<GameObject> playersOnEntrance = new HashSet<GameObject>();

    private void LiftEntrance()
    {
        transform.position = Vector2.MoveTowards(transform.position,nextLevelPosition.position,speed * Time.deltaTime);
        Debug.Log("Dang duoc nang");
    }

    private void Update() {
        if (playersOnEntrance.Count == 2 && canMove)
        {
            foreach (GameObject player in playersOnEntrance)
            {
                player.GetComponent<PlayerController>().CanMove = false;
            }
            LiftEntrance();
            if (transform.position == nextLevelPosition.position)
            {
                OnOutOfTheMap?.Invoke(this,EventArgs.Empty);
                Debug.Log("Loadnewlevel");
            }
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
