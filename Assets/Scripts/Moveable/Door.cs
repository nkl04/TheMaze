using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsTurnOn{get{return isTurnOn;} set{isTurnOn = value;}}
    
    [SerializeField] private Transform toOpenPoint;
    [SerializeField] private float openSpeed = 1f;

    private bool isTurnOn;

    private Vector3 startPosition;

    private void Start() {
        startPosition = transform.position;
    }
    private void Update() {
        if (isTurnOn)
        {
            transform.position = Vector2.MoveTowards(transform.position, toOpenPoint.position,openSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition,openSpeed * Time.deltaTime);
        }
    }
}
