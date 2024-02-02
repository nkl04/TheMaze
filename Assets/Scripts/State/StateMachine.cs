using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine
{
    public IState IdleState {get{return idleState;}}
    public IState RunState {get{return runState;}}
    public IState JumpState {get{return jumpState;}}


    [SerializeField] private RunState runState;
    [SerializeField] private JumpState jumpState;
    [SerializeField] private IdleState idleState;
    public StateMachine(PlayerController player)
    {
        this.runState = new RunState(player);
        this.jumpState = new JumpState(player);
        this.idleState = new IdleState(player); 
    }

    public IState currentState {get;private set;}

    public void Initialize(IState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        currentState.Exit();
        currentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if(currentState != null)
        {
            currentState.Update();
        }
    }

    
}
