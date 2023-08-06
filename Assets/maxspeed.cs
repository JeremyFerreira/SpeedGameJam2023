using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxspeed : MonoBehaviour
{
    public float maxSpeed;
    public float percentageSpeed;
    public Rigidbody2D rb;
    public ScriptableValueFloat percentageSpeedData;
    private void FixedUpdate()
    {
        float speed = rb.velocity.magnitude;
        if (speed > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        percentageSpeed = (rb.velocity.magnitude / maxSpeed) * 100;
        percentageSpeedData.Value = percentageSpeed;
    }
}
