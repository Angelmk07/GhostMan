using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PlayerStat player;
    [SerializeField] private PlayerInputListener listener;
    [SerializeField] private Supporter support;
    private Movment movment = new();

    private void Awake()
    {
        listener.Construct(player, movment, support);
    }
}
