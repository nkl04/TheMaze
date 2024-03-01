using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Elevator[] elevatorArray;

    private void OnTriggerEnter2D(Collider2D other) {
        foreach (Elevator elevator in elevatorArray)
        {
            elevator.IsTurnOn = true; 
        }
         
    }

    private void OnTriggerExit2D(Collider2D other) {
        foreach (Elevator elevator in elevatorArray)
        {
            elevator.IsTurnOn = false; 
        }
    }
    
}
