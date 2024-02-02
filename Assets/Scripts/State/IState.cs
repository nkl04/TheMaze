using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public abstract void Enter();
    
        // code that runs when we first enter the state
    
    public abstract void Update();
    
        // per-frame logic, include condition to transition to a new state
    

    public abstract void Exit();

        // code that runs when we exit the state
    
}
