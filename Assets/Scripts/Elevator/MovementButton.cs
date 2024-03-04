using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour
{

    public enum Open{
        Elevator,
        Door
    }

    [SerializeField] private Elevator[] elevatorArray;
    [SerializeField] private Door[] doorArray;
    [SerializeField] Open open;

    private float timeToHoldButton = 0.5f;
    private float timeToHoldButtonCounter;

    private void Start() {
        LevelEntrance.Instance.OnTakePlayerToNextLevel += LevelEntrance_OnTakePlayerToNextLevel;
    }

    private void LevelEntrance_OnTakePlayerToNextLevel(object sender, EventArgs e)
    {
        foreach (Door door in doorArray)
        {
            door.IsTurnOn = false; 
        }
    }

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
            timeToHoldButtonCounter = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (open == Open.Door)
        {
            timeToHoldButtonCounter += Time.deltaTime;
            if (timeToHoldButtonCounter >= timeToHoldButton)
            {
                foreach (Door door in doorArray)
                {
                    door.IsTurnOn = true; 
                }
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
    }
    
}
