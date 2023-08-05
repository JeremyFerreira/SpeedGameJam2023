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

    public void InitilizeContainerScore (string playerId, string playerScore)
    {
        _playerId.text = playerId;
        _playerScore.text = playerScore;
    }
}
