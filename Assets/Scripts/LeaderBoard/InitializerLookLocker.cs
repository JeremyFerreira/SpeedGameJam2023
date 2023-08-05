using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitializerLookLocker : MonoBehaviour
{
    private Action _sessionIsConnect;

    [SerializeField]
    private UnityEvent _sessionFail;
    [SerializeField]
    private UnityEvent _sessionSuccess;
    private void Awake()
    {
        _sessionIsConnect += GetConnectSession;
        LeaderBoardUtility.StartSession(_sessionIsConnect);
    }

    private void GetConnectSession ()
    {
        if(LeaderBoardUtility.IsConnected)
        {
            _sessionSuccess?.Invoke();
        }
        else
        {
            _sessionFail?.Invoke();
        }
    }
}
