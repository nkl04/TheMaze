using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{

    public enum Mode
    {
        Hold,
        Toggle,
    }

    [SerializeField] private bool canOpenSwitch;
    [SerializeField] private bool canCloseSwitch;
    [SerializeField] private Mode mode;
    [SerializeField] private Moveable[] moveableGameObject;
    [SerializeField] private SwitchBehaviour remainButton;

    private bool isPressingSwitch = false;
    private HashSet<GameObject> playersOnEntrance = new HashSet<GameObject>();


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            
            playersOnEntrance.Add(other.gameObject);
            Debug.Log("add" +other.gameObject);
            Debug.Log(playersOnEntrance.Count);
            if (mode == Mode.Toggle)
            {
                isPressingSwitch = !isPressingSwitch;
                foreach (Moveable item in moveableGameObject)
                {
                    if (canOpenSwitch && !item.IsTurnOn)
                    {
                        item.IsTurnOn = !item.IsTurnOn;
                    }
                    else if(canCloseSwitch && item.IsTurnOn)
                    {
                        item.IsTurnOn = !item.IsTurnOn;
                    }
                }
            }
            else
            {
                foreach (Moveable item in moveableGameObject)
                {
                    if (canOpenSwitch && canCloseSwitch && !item.IsTurnOn)
                    {
                        item.IsTurnOn = !item.IsTurnOn;
                    }
                }
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playersOnEntrance.Remove(other.gameObject);
            if (mode == Mode.Hold)
            {
                if (remainButton != null)
                {
                    if (playersOnEntrance.Count == 0 && remainButton.GetGameObjectSet().Count == 0)
                    {
                        foreach (Moveable item in moveableGameObject)
                        {
                            if (canOpenSwitch && canCloseSwitch && item.IsTurnOn)
                            {
                                item.IsTurnOn = !item.IsTurnOn;
                            }
                        }
                    }
                }
                else
                {
                    if (playersOnEntrance.Count == 0)
                    {
                        foreach (Moveable item in moveableGameObject)
                        {
                            if (canOpenSwitch && canCloseSwitch && item.IsTurnOn)
                            {
                                item.IsTurnOn = !item.IsTurnOn;
                            }
                        }
                    }
                }
                
                
            }
            isPressingSwitch = false;
        }
    }

    public HashSet<GameObject> GetGameObjectSet()
    {
        return playersOnEntrance;
    }

    
}
