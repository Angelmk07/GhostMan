using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    public void UpdateScore(int st)
    {
        scoreText.text = $"Score: {st}";
    }
    public void UpdateBestScore(int st)
    {
        bestScoreText.text = $"Best Score: {st}";
    }
}
