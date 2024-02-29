using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Elevator elevator;

    private void OnTriggerEnter2D(Collider2D other) {
        elevator.IsTurnOn = true;  
    }

    private void OnTriggerExit2D(Collider2D other) {
        elevator.IsTurnOn = false;
    }
    
}
