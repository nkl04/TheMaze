using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public enum Open{
        Elevator,
        Door
    }

    [SerializeField] private Elevator[] elevatorArray;
    [SerializeField] private Door[] doorArray;
    [SerializeField] Open open;

    private void OnTriggerEnter2D(Collider2D other) {
        if (open == Open.Elevator)
        {
            foreach (Elevator elevator in elevatorArray)
            {
                elevator.IsTurnOn = true; 
            }
        }
        else
        {
            foreach (Door door in doorArray)
            {
                door.IsTurnOn = true; 
            }
        }
        
         
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (open == Open.Elevator)
        {
            foreach (Elevator elevator in elevatorArray)
            {
                elevator.IsTurnOn = false; 
            }
        }
        else
        {
            foreach (Door door in doorArray)
            {
                door.IsTurnOn = false; 
            }
        }
    }
    
}
