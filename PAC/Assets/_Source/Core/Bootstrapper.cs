using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PlayerStat player;
    [SerializeField] private PlayerInputListener listener;
    [SerializeField] private Supporter support;

    [SerializeField] private PointCollector collector;
    [SerializeField] private LiveView liveView;
    [SerializeField] private ScoreView scoreView;
    private Movment movment = new();

    private void Awake()
    {
        listener.Construct(player, movment, support);
        player.takehit += liveView.LostLive;
        collector.ChangeScore += scoreView.UpdateBestScore;
        collector.ChangeBestScore += scoreView.UpdateScore;
    }



}
