using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeGrab : MonoBehaviour
{
    void Awake()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }

}
