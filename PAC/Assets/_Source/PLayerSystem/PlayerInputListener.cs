using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputListener : MonoBehaviour
{
    [SerializeField] private Vector2 moveInput = Vector2.zero;
    private PlayerStat _player;
    private Movment _movment;
    private Player _moveAction;
    private Supporter _supporter;
    public void Construct(PlayerStat playerStat, Movment movment,Supporter supporter)
    {
        _player = playerStat;
        _movment = movment;
        _supporter = supporter;
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

        if ( moveInput == Vector2.zero||(Mathf.Abs(moveInput.x) == Mathf.Abs(input.x) && Mathf.Abs(moveInput.y) == Mathf.Abs(input.y)))
        {
             moveInput = input;
        }
        else if ((_supporter != null && _supporter.CanChange))
        {
            moveInput = input;
        }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {

        _movment.Move(_player.playerObj, _player.speed, -moveInput*3);
        moveInput = Vector2.zero;
    }
}
