using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEffect : MonoBehaviour
{
    public UnityEvent OnCollisionDetected;

    [SerializeField] LayerMask targetLayers;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayers == (targetLayers | (1 << collision.gameObject.layer)))
        {
            OnCollisionDetected?.Invoke();
            print("Ouai");
        }
    }

}
