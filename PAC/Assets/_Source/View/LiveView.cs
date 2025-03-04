using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private void UpdateInfo(string st)
    {
        text.text = "Live: " + st;
    }
}
