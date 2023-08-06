using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpeedZoom : MonoBehaviour
{
    [SerializeField] ScriptableValueFloat _percentSpeed;
    [SerializeField] float _lerpSpeed = 2.0f; // Vitesse de l'interpolation
    [SerializeField] float _delayTime = 0.5f; // Délai avant de commencer l'interpolation
    private float _targetWidth;
    private float _startDelayTime;
    private CinemachineFollowZoom _followZoom;

    void Start()
    {
        _followZoom = GetComponent<CinemachineFollowZoom>();
        _targetWidth = _followZoom.m_Width;
        _startDelayTime = Time.time;
    }

    void Update()
    {
        float newTargetWidth = _percentSpeed.Value;

        // Démarrer l'interpolation après le délai
        if (Time.time - _startDelayTime >= _delayTime)
        {
            _targetWidth = Mathf.Lerp(_targetWidth, newTargetWidth, Time.deltaTime * _lerpSpeed);
        }

        _followZoom.m_Width = _targetWidth;
    }
}