using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _finishPanel;
    [SerializeField] GameObject _pausePanel;

    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] InputButtonScriptableObject _anyKey;
    [SerializeField] InputButtonScriptableObject _pauseKey;

    private void OnEnable()
    {
        _anyKey.OnValueChanged += StartGame;
        InputManager.Instance.ActiveGameInputs(false);
        _pauseKey.OnValueChanged += Pause;
    }
    private void OnDisable()
    {
        _anyKey.OnValueChanged -= StartGame;
        _pauseKey.OnValueChanged -= Pause;
    }

    public void StartGame(bool value)
    {
        _startPanel.SetActive(false);
        _gamePanel.SetActive(true);
        InputManager.Instance.ActiveGameInputs(true);
    }

    public void FinishGame()
    {
        _gamePanel.SetActive(false);
        _finishPanel.SetActive(true);
    }

    public void Pause(bool value)
    {
        if (value)
        {
            Time.timeScale = 0f;
            InputManager.Instance.ActiveGameInputs(false);
            _pausePanel.SetActive(true);
            _gamePanel.SetActive(false);
        }
    }

    public void Resume(bool value)
    {
        if (value)
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
            _gamePanel.SetActive(true);
            InputManager.Instance.ActiveGameInputs(true);
        }
    }

    public void UpdateTimer(float time)
    {
        _timerText.text = TimerFormat.FormatTime(time);
    }
}
