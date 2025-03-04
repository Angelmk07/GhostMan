using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private void UpdateScore(string st)
    {
        scoreText.text = "Score: " + st;
    }
    private void UpdateBestScore(string st)
    {
        bestScoreText.text = "Best Score: " + st;
    }
}
