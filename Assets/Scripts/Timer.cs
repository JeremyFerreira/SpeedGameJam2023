using LootLocker.Requests;
using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static float _actualTime;
    public static float GetTime {  get { return _actualTime; } }

    private bool _isPlaying;

    public void StartTimer ()
    {
        ResetTimer();
        _isPlaying = true;
    }

    public void StopTimer ()
    {
        _isPlaying = false;
    }

    public void RestartTimer ()
    {
        _isPlaying = true;
    }
    public void ResetTimer ()
    {
        _actualTime = 0;
    }

    private void FixedUpdate()
    {
        if (_isPlaying)
        {
            _actualTime += Time.deltaTime;
        }
    }




}
