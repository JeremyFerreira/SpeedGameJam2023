using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private static float _actualTime;
    public UnityEvent<float> OnValueChange;
    public static float GetTime {  get { return _actualTime; } }

    private bool _isPlaying;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer ()
    {
        ResetTimer();
        _isPlaying = true;
    }

    public void StopTimer ()
    {
        _isPlaying = false;
    }

    public void ResumeTimer ()
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
            OnValueChange?.Invoke(_actualTime);
        }
    }
}
