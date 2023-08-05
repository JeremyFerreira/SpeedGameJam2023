using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _finishPanel;
    [SerializeField] GameObject _pausePanel;

    [SerializeField] TextMeshProUGUI _timerText;

    public void StartGame()
    {
        _startPanel.SetActive(false);
        _gamePanel.SetActive(true);
    }

    public void FinishGame()
    {
        _gamePanel.SetActive(false);
        _finishPanel.SetActive(true);
    }

    public void Pause()
    {
        _pausePanel.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void Resume()
    {
        _pausePanel.SetActive(false);
        _gamePanel.SetActive(true);
    }

    public void UpdateTimer(float time)
    {
        _timerText.text = TimerFormat.FormatTime(time);
    }

}
