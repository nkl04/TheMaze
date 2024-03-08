using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Horizontal,
    Vertical
}

public class Elevator : MonoBehaviour
{
    public bool IsTurnOn{get{return isTurnOn;} set{isTurnOn = value;}}

    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform upperPos;
    [SerializeField] private Transform downPos;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Direction direction;

    private bool isTurnOn;
    private bool isMoveDown;
    private bool isMoveRight;
    
    private Vector3 initPosition;


    private void Start() {
        initPosition = transform.position;
    }

    private void Update() {
        if (direction == Direction.Horizontal)
        {
            MoveHorizontal();
        }
        else
        {
            MoveVertical();
        }
    }

    private void MoveHorizontal()
    {
        if (isTurnOn)
        {  
            if (transform.position.x == leftPos.position.x)
            {
                isMoveRight = true;
            }
            else if(transform.position.x == rightPos.position.x)
            {
                isMoveRight = false;
            }

            if (isMoveRight)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
        else{
            MoveToInitialPosition();
        }
    }

    private void MoveVertical()
    {
        if (isTurnOn)
        {  
            if (transform.position.y == upperPos.position.y)
            {
                isMoveDown = true;
            }
            else if(transform.position.y == downPos.position.y)
            {
                isMoveDown = false;
            }

            if (isMoveDown)
            {
                MoveDown();
            }
            else
            {
                MoveUp();
            }
        }
        else{
            MoveToInitialPosition();
        }
    }

    private void MoveUp()
    {
        transform.position = Vector2.MoveTowards(transform.position,upperPos.position,speed * Time.deltaTime);
    }

    private void MoveDown()
    {
        transform.position = Vector2.MoveTowards(transform.position,downPos.position,speed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        transform.position = Vector2.MoveTowards(transform.position,leftPos.position,speed * Time.deltaTime);
    }
    
    private void MoveRight()
    {
        transform.position = Vector2.MoveTowards(transform.position,rightPos.position,speed * Time.deltaTime);
    }

    private void MoveToInitialPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position,initPosition,speed * Time.deltaTime);
    }

    public Direction GetDirection()
    {
        return direction;
    }
}
