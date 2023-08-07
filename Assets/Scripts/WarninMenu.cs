using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarninMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _title;
    [SerializeField]
    private float _time;
    [SerializeField]
    private GameObject _controller;
    [SerializeField]
    private float _time2;
    [SerializeField]
    private GameObject _wifi;
    [SerializeField]
    private float _time3;

    [SerializeField]
    private UnityEvent _onWarningFinish;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineTiltle());
    }

    IEnumerator CoroutineTiltle()
    {
        _title.SetActive(true);
        yield return new WaitForSeconds(_time);
        _title.SetActive(false);
        StartCoroutine(CoroutineController());
    }

    IEnumerator CoroutineController()
    {
        _controller.SetActive(true);
        yield return new WaitForSeconds(_time2);
        _controller.SetActive(false);
        StartCoroutine(CoroutineWifi());
    }
    IEnumerator CoroutineWifi()
    {
        _wifi.SetActive(true);
        yield return new WaitForSeconds(_time3);
        _wifi.SetActive(false);
        _onWarningFinish?.Invoke();
    }
}
