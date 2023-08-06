using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContainerScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerId;
    [SerializeField]
    private TextMeshProUGUI _playerScore;
    [SerializeField]
    private TextMeshProUGUI _playerRank;

    public void InitilizeContainerScore (string playerId, string playerScore, string rank)
    {
        _playerId.text = playerId;
        _playerScore.text = playerScore;
        _playerRank.text = rank;
    }
}
