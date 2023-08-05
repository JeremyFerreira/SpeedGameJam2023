using LootLocker;
using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class LeaderBoardUtility
{
    private static string _leaderBoardKey = "tst";
    private static int _leaderboardID = 16634;

    private static bool _isConnected;
    private static int _playerID;
    private static string _playerName;

    public static bool IsConnected { get { return _isConnected; } }
    public static int PlayerID { get { return _playerID; } }

    public static string PlayerName { get { return _playerName; } set { _playerName = value; } }

    private static Action<LootLockerGuestSessionResponse> _resquestSartGuestSessions;

    public static void StartSession (Action toinvoke)
    {
        
        _resquestSartGuestSessions += ReceveReponseGuestSession;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.statusCode == 200)
            {
                _playerID = response.player_id;
                _isConnected = true;
                toinvoke?.Invoke();
            }
            else
            {

                _isConnected = false;
                toinvoke?.Invoke();
            }
        });
    }

    private static void ReceveReponseGuestSession (LootLockerGuestSessionResponse response)
    {  
        _resquestSartGuestSessions -= ReceveReponseGuestSession;

        _isConnected = response.success;

        if(_isConnected)
        {
            _playerID = response.player_id;
            Debug.Log(response.player_id);
            Debug.Log(response.player_identifier);
        }
    }

    private static Action<LootLockerSubmitScoreResponse> _resquestSubmitScore;

    public static void SubmitToLeaderBoard (string memberID, int score, Action<bool> finishAdd)
    {
        if(!_isConnected) { finishAdd?.Invoke(false); return; }

        _resquestSubmitScore += ReceveReponseSubmitScore;
        LootLockerSDKManager.SubmitScore(memberID, score, _leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                finishAdd?.Invoke(true);
            }
            else
            {
                finishAdd?.Invoke(false);
            }
        });
        
    }

    private static void ReceveReponseSubmitScore (LootLockerSubmitScoreResponse reponse)
    {
        _resquestSubmitScore -= ReceveReponseSubmitScore;

        Debug.Log(reponse.success);
    }

    public static void GetLeaderBoard (Action<Dictionary<string, int>> toReturn)
    {
        Dictionary<string,int> listLeader = new Dictionary<string,int>();
        LootLockerSDKManager.GetScoreList(_leaderBoardKey, 10, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful"+ response.items.Length);
                for(int i = 0; i<response.items.Length;i++)
                {
                    listLeader.Add(response.items[i].member_id, response.items[i].score);
                }
                toReturn?.Invoke(listLeader);
            }
            else
            {
                Debug.Log("failed: " + response.Error);
                toReturn?.Invoke(listLeader);
            }
        });
    }






    private static Action<LootLockerPlayerFilesResponse> requestPlayerFile;

    public static void ChangePlayerName (string newPlayerName)
    {
        requestPlayerFile += ReceveReponsePlayerFile;
        LootLockerSDKManager.GetAllPlayerFiles(_playerID, requestPlayerFile);
    }

    private static void ReceveReponsePlayerFile (LootLockerPlayerFilesResponse reponsePlayerFile)
    {
        Debug.Log(reponsePlayerFile.success +" "+ reponsePlayerFile.items.Length);
        requestPlayerFile -= ReceveReponsePlayerFile;
        for (int i = 0; i < reponsePlayerFile.items.Length; i++)
        {
            Debug.Log(reponsePlayerFile.items[i].name);
            Debug.Log(reponsePlayerFile.items[i].id);
            Debug.Log(reponsePlayerFile.items[i].revision_id);
        }
    }








}
