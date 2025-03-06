using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveView : MonoBehaviour
{
    [SerializeField] private GameObject[] lives;
    public void LostLive()
    {
        if (lives.Length != 0)
        Destroy(lives[lives.Length-1]);
    }
}
