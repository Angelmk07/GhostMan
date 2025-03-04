using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStat : MonoBehaviour
{
    public Action takehit;
    [field:SerializeField] public int live {  get; private set; }
    [field:SerializeField] public GameObject playerObj {  get; private set; } 
    [field: SerializeField] public float speed {  get; private set; }
    [field: SerializeField] public Rigidbody2D rb {  get; private set; }

    private void Awake()
    {
        playerObj = gameObject;
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeHit()
    {
        live--;
        takehit.Invoke();
    }
}
public enum PlayerDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}