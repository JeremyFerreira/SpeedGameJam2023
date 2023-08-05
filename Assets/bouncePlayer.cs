using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncePlayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] float resetTimer = 0.05f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Vector2 dirToPoint = -(new Vector2(transform.position.x, transform.position.y) - other.ClosestPoint(transform.position)).normalized;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPoint, 1000);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(hit.normal * jumpForce, ForceMode2D.Impulse);
        }
    }
}
