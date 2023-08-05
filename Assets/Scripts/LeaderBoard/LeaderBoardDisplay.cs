using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LeaderBoardDisplay : MonoBehaviour
{
    private Dictionary<string, int> _leaderDic;

    private Action<Dictionary<string, int>> _actionGetLeaderBoard;

    [SerializeField]
    private GameObject _prefabContainer;
    [SerializeField]
    private Transform _parentContainer;

    [SerializeField]
    private UnityEvent _leaderBoardSuccessDisplay;
    [SerializeField]
    private UnityEvent _leaderBoardFailDisplay;

    [SerializeField]
    private GameObject _panelSubmition;
    [SerializeField]
    private TMP_InputField _textPlayerId;

    [SerializeField]
    private UnityEvent _submittedScoreSuccess;
    [SerializeField]
    private UnityEvent _submittedScoreFail;

    public void ShowLeaderBoard ()
    {
        if (!LeaderBoardUtility.IsConnected) { return; }
        _parentContainer.gameObject.SetActive(true);
        _actionGetLeaderBoard += GetLeaderBoard;

        LeaderBoardUtility.GetLeaderBoard(_actionGetLeaderBoard);
        
    }

    private void GetLeaderBoard (Dictionary<string, int> newDic)
    {
        _leaderDic = newDic;
        if(_leaderDic == null) { _leaderBoardFailDisplay?.Invoke(); return; }
        SpawnContainer();
    }

    private void SpawnContainer ()
    {
        for(int i = 0; i< _leaderDic.Count;i++)
        {
            ContainerScore containerScore = Instantiate(_prefabContainer, _parentContainer).GetComponent<ContainerScore>();
            containerScore.InitilizeContainerScore(_leaderDic.ElementAt(i).Key,_leaderDic.ElementAt(i).Value.ToString());
        }
        _leaderBoardSuccessDisplay?.Invoke();
    }

    public void ShowSubmitLeaderBoard ()
    {
        if (!LeaderBoardUtility.IsConnected) { return; }
        
        _panelSubmition.SetActive(true);
        _textPlayerId.text = LeaderBoardUtility.PlayerID.ToString();


    }

    public void UnShowSubmitLeaderBoard()
    {
        _panelSubmition.SetActive(false);
    }

    public void SubmitScore ()
    {
        int score = 250;
        string id;
        if (LeaderBoardUtility.PlayerName == "") { id = LeaderBoardUtility.PlayerID.ToString(); }
        else { id = LeaderBoardUtility.PlayerName; }
        Debug.Log(id);
       
        //Recuper le score
        LeaderBoardUtility.SubmitToLeaderBoard(id, score, (response) =>
        {
            if(response)
            {
                _submittedScoreSuccess?.Invoke();
            }
            else
            {
                _submittedScoreFail?.Invoke();
            }
        });
    }

    public void ChangePlayerID (string playerID)
    {
        LeaderBoardUtility.PlayerName = playerID;
    }

    
}
