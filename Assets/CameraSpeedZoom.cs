using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpeedZoom : MonoBehaviour
{
    [SerializeField] ScriptableValueFloat _percentSpeed;
    [SerializeField] float _lerpSpeed = 2.0f; // Vitesse de l'interpolation
    private float _targetWidth; // Valeur de largeur cible pour l'interpolation
    private CinemachineFollowZoom _followZoom;

    void Start()
    {
        _followZoom = GetComponent<CinemachineFollowZoom>();
        _targetWidth = _followZoom.m_Width;
    }

    void Update()
    {
        float newTargetWidth = _percentSpeed.Value;
        _targetWidth = Mathf.Lerp(_targetWidth, newTargetWidth, Time.deltaTime * _lerpSpeed);
        _followZoom.m_Width = _targetWidth;
    }
}
