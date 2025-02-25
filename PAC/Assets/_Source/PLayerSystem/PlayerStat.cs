using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStat : MonoBehaviour
{
    [field:SerializeField] public int live {  get; private set; }
    [field:SerializeField] public GameObject playerObj {  get; private set; } 
    [field: SerializeField] public float speed {  get; private set; }

    private void Awake()
    {
        playerObj = gameObject;
    }
}
