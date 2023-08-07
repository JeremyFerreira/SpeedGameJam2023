using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoDestroyParticule : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
}
