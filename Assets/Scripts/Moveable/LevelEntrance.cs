using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntrance : MonoBehaviour
{
    
    [SerializeField] private float speed;

    private HashSet<GameObject> playersOnEntrance = new HashSet<GameObject>();
    private void LiftEntrance()
    {
        transform.position = Vector2.MoveTowards(transform.position,transform.position + Vector3.up*5,speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            playersOnEntrance.Add(other.gameObject);

            // Kiểm tra nếu cả hai player đều đứng lên trên cổng
            if (playersOnEntrance.Count == 2)
            {
                LiftEntrance();
            }
        }
    }
}
