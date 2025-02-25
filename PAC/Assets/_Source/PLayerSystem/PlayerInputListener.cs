using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputListener : MonoBehaviour
{
    private PlayerStat _player;
    private Movment _movment;
    private Player _moveAction;
    private Vector2 moveInput;
    public void Construct(PlayerStat playerStat, Movment movment)
    {
        _player = playerStat;
        _movment = movment;
 
    }
    private void OnEnable()
    {
        _moveAction = new();
        _moveAction.Enable();
        _moveAction.InputMovment.MoveHorizontal.performed += OnMove;
        _moveAction.InputMovment.MoveVertical.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = input;
    }

    private void OnDisable()
    {
        _moveAction.InputMovment.MoveVertical.performed -= OnMove;
        _moveAction.InputMovment.MoveHorizontal.performed -= OnMove;
        _moveAction.Disable();

    }
    private void Update()
    {
        _movment.Move(_player.playerObj, _player.speed, moveInput);
    }
}
