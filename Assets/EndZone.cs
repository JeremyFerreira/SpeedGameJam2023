using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZone : MonoBehaviour
{
    public UnityEvent OnReachFinish;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            OnReachFinish?.Invoke();
        }
    }
}
