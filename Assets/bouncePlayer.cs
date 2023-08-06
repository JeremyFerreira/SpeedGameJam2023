using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncePlayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] float resetTimer = 0.05f;
    [SerializeField] List<GameObject> gameObjectsCollide;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            Vector2 dirToPoint = -(new Vector2(transform.position.x, transform.position.y) - other.ClosestPoint(transform.position)).normalized;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPoint, 1000);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(hit.normal * jumpForce, ForceMode2D.Impulse);
            if (other.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb))
            {
                otherRb.AddForceAtPosition(-rb.velocity * jumpForce, hit.point);
            }
            gameObjectsCollide.Add(other.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            if (!gameObjectsCollide.Contains(other.gameObject))
            {
                Vector2 dirToPoint = -(new Vector2(transform.position.x, transform.position.y) - other.ClosestPoint(transform.position)).normalized;
                RaycastHit2D hit;
                hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPoint, 1000);
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(hit.normal * jumpForce, ForceMode2D.Impulse);
                if (other.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb))
                {
                    otherRb.AddForceAtPosition(-rb.velocity * jumpForce, hit.point);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            if (!gameObjectsCollide.Contains(other.gameObject))
            {
                Vector2 dirToPoint = -(new Vector2(transform.position.x, transform.position.y) - other.ClosestPoint(transform.position)).normalized;
                RaycastHit2D hit;
                hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPoint, 1000);
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(hit.normal * jumpForce, ForceMode2D.Impulse);
                if (other.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb))
                {
                    otherRb.AddForceAtPosition(-rb.velocity * jumpForce, hit.point);
                }

            }
        }

        if (gameObjectsCollide.Contains(other.gameObject))
        {
            gameObjectsCollide.Remove(other.gameObject);
        }
    }
}
