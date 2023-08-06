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

    private Action<bool> _changeName;

    List<GameObject> containers;

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

    [SerializeField]
    private UnityEvent _changeNameSucess;
    [SerializeField]
    private UnityEvent _changeNameFail;


    public void ShowPanelLeaderBoard (bool showSubmit)
    {
        if (showSubmit) { _panelSubmition.SetActive(true); }
        _parentContainer.gameObject.SetActive(true);
    }

    public void ShowLeaderBoard ()
    {
        if (!LeaderBoardUtility.IsConnected) { return; }
        _parentContainer.gameObject.SetActive(true);
        _actionGetLeaderBoard += GetLeaderBoard;
        DestroyAllContainers();
        LeaderBoardUtility.GetLeaderBoard(_actionGetLeaderBoard);
        
    }

    private void DestroyAllContainers ()
    {
        if(containers != null)
        {
            for(int i = 0;i <  containers.Count;i++)
            {
                Destroy(containers[i]);
            }
        }
    }

    private void GetLeaderBoard (Dictionary<string, int> newDic)
    {
        _leaderDic = newDic;
        if(_leaderDic == null) { _leaderBoardFailDisplay?.Invoke(); return; }
        SpawnContainer();
    }



    private void SpawnContainer ()
    {
        containers = new List<GameObject>();
        for (int i = 0; i< _leaderDic.Count;i++)
        {
            ContainerScore containerScore = Instantiate(_prefabContainer, _parentContainer).GetComponent<ContainerScore>();
            containerScore.InitilizeContainerScore(_leaderDic.ElementAt(i).Key,_leaderDic.ElementAt(i).Value.ToString());
            containers.Add(containerScore.gameObject);
        }
        _leaderBoardSuccessDisplay?.Invoke();
    }

    public void ShowSubmitLeaderBoard ()
    {
        if (!LeaderBoardUtility.IsConnected) { return; }
        
        _panelSubmition.SetActive(true);
        _textPlayerId.text = LeaderBoardUtility.PlayerName.ToString();


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
        else { id = LeaderBoardUtility.PlayerName; }//+ random entre 100 et 999; }
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

    public void ChangePlayerName (string playernName)
    {
        _changeName += NameChanged;
        LeaderBoardUtility.ChangePlayerName(playernName, _changeName);
    }

    private void NameChanged (bool isChange)
    {
        _changeName -= NameChanged;
        if (isChange)
        {
            Debug.Log("mon nom est changer");
            _changeNameSucess?.Invoke();
        }
        else
        {
            _changeNameFail?.Invoke();
        }
    }

    
}
