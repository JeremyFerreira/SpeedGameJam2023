using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _actualTime;
    public float GetTime {  get { return _actualTime; } }

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
        }
    }
}
