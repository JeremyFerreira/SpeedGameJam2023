using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartZone : MonoBehaviour
{
    public UnityEvent OnExitStartZone;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            OnExitStartZone?.Invoke();
        }
    }
}
