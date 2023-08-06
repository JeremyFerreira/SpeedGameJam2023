using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpeedZoom : MonoBehaviour
{
    [SerializeField] ScriptableValueFloat _percentSpeed;
    [SerializeField] float _lerpSpeed = 2.0f; // Vitesse de l'interpolation
    [SerializeField] float _threshold = 0.1f; // Seuil pour déclencher le changement de largeur
    [SerializeField] float _delay = 0.5f; // Délai avant de réagir aux changements de vitesse
    private float _targetWidth; // Valeur de largeur cible pour l'interpolation
    private float _lastSpeedChangeTime; // Heure du dernier changement de vitesse
    private CinemachineFollowZoom _followZoom;

    void Start()
    {
        _followZoom = GetComponent<CinemachineFollowZoom>();
        _targetWidth = _followZoom.m_Width;
        _lastSpeedChangeTime = Time.time;
    }

    void Update()
    {
        float newTargetWidth = _percentSpeed.Value + 32f;

        // Si le changement de vitesse est significatif et le délai est passé
        if (Mathf.Abs(newTargetWidth - _targetWidth) > _threshold &&
            Time.time - _lastSpeedChangeTime > _delay)
        {
            _lastSpeedChangeTime = Time.time;
            _targetWidth = newTargetWidth;
        }
        else
        {
            // Sinon, effectuez une interpolation lente vers le nouveau zoom
            _targetWidth = Mathf.Lerp(_targetWidth, newTargetWidth, Time.deltaTime * _lerpSpeed);
        }

        _followZoom.m_Width = _targetWidth;
    }
}
