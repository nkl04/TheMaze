using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get;private set;}

    public enum Binding{
        Player1_Jump,
        Player1_MoveDown,
        Player1_MoveLeft,
        Player1_MoveRight,
        Player2_Jump,
        Player2_MoveDown,
        Player2_MoveLeft,
        Player2_MoveRight
    }
    private PlayerInputSystem playerInputSystem;
    
    private void Awake() {
        Instance = this;
        playerInputSystem = new PlayerInputSystem();
    }

    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Player1_Jump:
                return playerInputSystem.Player1.Jump.bindings[0].ToDisplayString();
            case Binding.Player1_MoveLeft:
                return playerInputSystem.Player1.Move.bindings[1].ToDisplayString();
            case Binding.Player1_MoveRight:
                return playerInputSystem.Player1.Move.bindings[3].ToDisplayString();
            case Binding.Player1_MoveDown:
                return playerInputSystem.Player1.Move.bindings[2].ToDisplayString();
            case Binding.Player2_Jump:
                return playerInputSystem.Player2.Jump.bindings[0].ToDisplayString();
            case Binding.Player2_MoveLeft:
                return playerInputSystem.Player2.Move.bindings[1].ToDisplayString();
            case Binding.Player2_MoveRight:
                return playerInputSystem.Player2.Move.bindings[3].ToDisplayString();
            case Binding.Player2_MoveDown:
                return playerInputSystem.Player2.Move.bindings[2].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action action)
    {
        playerInputSystem.Player1.Disable();
        playerInputSystem.Player2.Disable();
         
        InputAction inputAction;
        int bindingIndex;
        switch(binding)
        {
            default:
            case Binding.Player1_Jump:
                inputAction = playerInputSystem.Player1.Jump;
                bindingIndex = 0;
                break;
            case Binding.Player1_MoveDown:
                inputAction = playerInputSystem.Player1.Move;
                bindingIndex = 2;
                break;
            case Binding.Player1_MoveLeft:
                inputAction = playerInputSystem.Player1.Move;
                bindingIndex = 1;
                break;
            case Binding.Player1_MoveRight:
                inputAction = playerInputSystem.Player1.Move;
                bindingIndex = 3;
                break;
            case Binding.Player2_Jump:
                inputAction = playerInputSystem.Player2.Jump;
                bindingIndex = 0;
                break;
            case Binding.Player2_MoveDown:
                inputAction = playerInputSystem.Player2.Move;
                bindingIndex = 2;
                break;
            case Binding.Player2_MoveLeft:
                inputAction = playerInputSystem.Player2.Move;
                bindingIndex = 1;
                break;
            case Binding.Player2_MoveRight:
                inputAction = playerInputSystem.Player2.Move;
                bindingIndex = 3;
                break;
            
        }
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>{
            callback.Dispose();
            playerInputSystem.Player1.Enable();
            playerInputSystem.Player2.Enable();
            action();
        }).Start();

        
    }




}
