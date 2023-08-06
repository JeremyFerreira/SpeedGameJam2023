using LootLocker;
using LootLocker.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
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

        LootLockerSDKManager.GetScoreList(_leaderBoardKey, 100, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful"+ response.items.Length);

               // response
                for (int i = 0; i<response.items.Length;i++)
                {
                    int score = response.items[i].score;
                    listLeader.Add(response.items[i].member_id, score);
                }

                Dictionary<string, int> listLeaderName = new Dictionary<string, int>();
                List<ulong> id = new List<ulong>();

                for (int i = 0; i < listLeader.Count; i++)
                {
                    id.Add(ulong.Parse(listLeader.ElementAt(i).Key));
                }

                LootLockerSDKManager.LookupPlayerNamesByPlayerIds(id.ToArray(), responseName =>
                {
                    if (responseName.success)
                    {
                        for (int i = 0; i < responseName.players.Length; i++)
                        {
                            listLeaderName.Add(responseName.players[i].name, listLeader.ElementAt(i).Value);
                        }

                        toReturn?.Invoke(listLeaderName);
                    }
                    else
                    {
                        toReturn?.Invoke(listLeader);
                    }
                });
            }
            else
            {
                Debug.Log("failed: " + response.Error);
                toReturn?.Invoke(listLeader);
            }
        });

        
    }

    private static void AddToDictionnary ()
    {

    }






    private static Action<LootLockerPlayerFilesResponse> requestPlayerFile;

    public static void ChangePlayerNametst (string newPlayerName)
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


    public static void ChangePlayerName (string newPlayerName, Action<bool> responseName)
    {
        LootLockerSDKManager.SetPlayerName(newPlayerName, (response) =>
        {
            if (response.success)
            {
                _playerName = newPlayerName;
                Debug.Log("Successfully set player name");
                responseName?.Invoke(true);
            }
            else
            {
                _playerName = newPlayerName;
                Debug.Log("Error setting player name");
                responseName?.Invoke(false);
            }
        });
    }

    public static void GetPlayerName (Action<bool> responseName)
    {
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                _playerName = response.name;
                Debug.Log("Successfully retrieved player name: " + response.name);
                responseName?.Invoke(true);
            }
            else
            {
                Debug.Log("Error getting player name");
                responseName?.Invoke(false);
            }
        });
    }

    public static void GetPlayerName()
    {
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                _playerName = response.name;
                Debug.Log("Successfully retrieved player name: " + response.name);
            }
            else
            {
                Debug.Log("Error getting player name");
            }
        });
    }








}
