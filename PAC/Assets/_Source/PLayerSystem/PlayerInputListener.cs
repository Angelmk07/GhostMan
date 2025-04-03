using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;

public class PlayerInputListener : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector2 moveInput = Vector2.zero;
    [SerializeField] private Transform teleportEntryLeft;
    [SerializeField] private Transform teleportExitRight;
    [SerializeField] private Transform teleportEntryRight;
    [SerializeField] private Transform teleportExitLeft;
    private PlayerStat _player;
    private Movment _movment;
    private Player _moveAction;
    private Supporter _supporter;

    public void Construct(PlayerStat playerStat, Movment movment, Supporter supporter)
    {
        _player = playerStat;
        _movment = movment;
        _supporter = supporter;
    }

    private void OnEnable()
    {
        _moveAction = new();
        _moveAction.Enable();
        _moveAction.InputMovment.MoveHorizontal.canceled += OnMove;
        _moveAction.InputMovment.MoveVertical.canceled += OnMove;
        _moveAction.InputMovment.MoveHorizontal.performed += OnMove;
        _moveAction.InputMovment.MoveVertical.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (moveInput == Vector2.zero || (Mathf.Abs(moveInput.x) == Mathf.Abs(input.x) && Mathf.Abs(moveInput.y) == Mathf.Abs(input.y)))
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
        CheckTeleport();
    }

    private void CheckTeleport()
    {
        Vector3 position = _player.playerObj.transform.position;
        if (Vector3.Distance(position, teleportEntryLeft.position) < 0.5f)
        {
            _player.playerObj.transform.position = teleportExitRight.position;
        }
        else if (Vector3.Distance(position, teleportEntryRight.position) < 0.5f)
        {
            _player.playerObj.transform.position = teleportExitLeft.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 playerPosition = _player.playerObj.transform.position;
        Vector3Int cellPosition = tilemap.WorldToCell(playerPosition);
        Vector3 nearestTilePosition = tilemap.GetCellCenterWorld(cellPosition);
        _player.playerObj.transform.position = nearestTilePosition;
        moveInput = Vector2.zero;
    }
}
