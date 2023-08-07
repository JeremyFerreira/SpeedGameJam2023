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
    private TextMeshProUGUI _textScore;

    [SerializeField]
    private UnityEvent _submittedScoreSuccess;
    [SerializeField]
    private UnityEvent _submittedScoreFail;

    [SerializeField]
    private UnityEvent _changeNameSucess;
    [SerializeField]
    private UnityEvent _changeNameFail;

    [SerializeField]
    private GameObject _panelLeaderBoard;
    [SerializeField]
    private GameObject _panelNotConnected;

    [SerializeField]
    private UnityEvent _onLeaderPanelOpen;

    [SerializeField]
    private MenuManager menuManager;


    public void UnShowPanelLeaderBoard ()
    {
        if (!LeaderBoardUtility.IsConnected) { _leaderBoardFailDisplay?.Invoke(); }
        _panelLeaderBoard.SetActive(false);
        UnShowSubmitLeaderBoard();
        _parentContainer.gameObject.SetActive(false);
    }

    public void ShowPanelNotConnected ()
    {
        UnShowSubmitLeaderBoard();
        UnShowLeaderBoard();
        _panelNotConnected.SetActive(true);
    }

    public void UnShowPanelNotConnected()
    {
      
        _panelNotConnected.SetActive(false);
    }

    public void ShowPanelLeaderBoard (bool showSubmit)
    {
        _onLeaderPanelOpen?.Invoke();
        _panelLeaderBoard.SetActive(true);
        Time.timeScale = 0;
        if (!LeaderBoardUtility.IsConnected) { ShowPanelNotConnected(); return; }
        UnShowPanelNotConnected();
        if (showSubmit) { ShowSubmitLeaderBoard();
            menuManager.enabled = false;
        }
        ShowLeaderBoard();
    }

    public void UnShowLeaderBoard ()
    {
        _parentContainer.gameObject.SetActive(false);
    }

    public void ShowLeaderBoard ()
    {
        if (!LeaderBoardUtility.IsConnected) { ShowPanelNotConnected(); return; }
        DestroyAllContainers();
        _parentContainer.gameObject.SetActive(true);
    }

    private void DestroyAllContainers ()
    {
        if(containers != null)
        {
            Debug.Log("je detruit EEEEEEEEE");
            for(int i = 0;i <  containers.Count;i++)
            {
                Destroy(containers[i]);
            }
        }

        _actionGetLeaderBoard += GetLeaderBoard;

        LeaderBoardUtility.GetLeaderBoard(_actionGetLeaderBoard);
    }

    private void GetLeaderBoard (Dictionary<string, int> newDic)
    {
        _actionGetLeaderBoard -= GetLeaderBoard;
        _leaderDic = newDic;
        if(_leaderDic == null) { _leaderBoardFailDisplay?.Invoke(); return; }
        SpawnContainer();
    }



    private void SpawnContainer ()
    {
        containers = new List<GameObject>();
        for (int i = 0; i< _leaderDic.Count;i++)
        {
            GameObject container = Instantiate(_prefabContainer, _parentContainer);
            ContainerScore containerScore = container.GetComponent<ContainerScore>();
            string playerId = _leaderDic.ElementAt(i).Key.Split('/')[1];
            float time = _leaderDic.ElementAt(i).Value / 100f;

            containerScore.InitilizeContainerScore(playerId, TimerFormat.FormatTime(time), (i+1).ToString());
            containers.Add(container);
        }
        _leaderBoardSuccessDisplay?.Invoke();
    }

    public void ShowSubmitLeaderBoard ()
    {
        if (!LeaderBoardUtility.IsConnected) { return; }
        
        _panelSubmition.SetActive(true);
        _textPlayerId.text = LeaderBoardUtility.PlayerName;

        float score = Mathf.RoundToInt(Timer.GetTime * 100);
        score /= 100;

        _textScore.text = TimerFormat.FormatTime(score);

    }

    public void UnShowSubmitLeaderBoard()
    {
        _panelSubmition.SetActive(false);
    }

    public void SubmitScore ()
    {
        int score = Mathf.RoundToInt(Timer.GetTime * 100);
        string id;
        if (LeaderBoardUtility.PlayerName == "") { id = LeaderBoardUtility.PlayerID.ToString(); }
        else { id = LeaderBoardUtility.PlayerName; }//+ random entre 100 et 999; }
        Debug.Log(id);
        Debug.Log(score);
       
        //Recuper le score
        LeaderBoardUtility.SubmitToLeaderBoard(LeaderBoardUtility.PlayerID.ToString(), score, (response) =>
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
