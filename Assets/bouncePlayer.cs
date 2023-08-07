using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncePlayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] float resetTimer = 0.05f;
    [SerializeField] List<GameObject> gameObjectsCollide;
    bool InTriggerStay;
    bool InBounceCoroutine;
    public GameObject particuleBounce;
    public SoundeffectPlay sound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            Vector2 ContactPoint = other.ClosestPoint(transform.position);
            Vector2 dirToPoint = -(new Vector2(transform.position.x, transform.position.y) - ContactPoint).normalized;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPoint, 1000);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(hit.normal * jumpForce, ForceMode2D.Impulse);
            gameObjectsCollide.Add(other.gameObject);
            StartCoroutine(RemoveObject(other));
            Instantiate(particuleBounce, new Vector3(ContactPoint.x,ContactPoint.y, 1), Quaternion.identity);
            sound.PlaySoundEffectRandom(9, 11);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        InBounceCoroutine = true;
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            if (!gameObjectsCollide.Contains(other.gameObject))
            {
                Vector2 ContactPoint = other.ClosestPoint(transform.position);
                Vector2 dirToPoint = -(new Vector2(transform.position.x, transform.position.y) - ContactPoint).normalized;
                RaycastHit2D hit;
                hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPoint, 1000);
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(hit.normal * jumpForce, ForceMode2D.Impulse);
                Instantiate(particuleBounce, new Vector3(ContactPoint.x, ContactPoint.y, 0), Quaternion.identity);
                sound.PlaySoundEffectRandom(9, 11);
            }
            StartCoroutine(RemoveObject(other));
        }
        
    }
    IEnumerator RemoveObject(Collider2D other)
    {
        yield return new WaitForSeconds(0.2f);
        if (gameObjectsCollide.Contains(other.gameObject))
        {
            gameObjectsCollide.Remove(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            if (!gameObjectsCollide.Contains(other.gameObject))
            {
                Vector2 ContactPoint = other.ClosestPoint(transform.position);
                Vector2 dirToPoint = -(new Vector2(transform.position.x, transform.position.y) - ContactPoint).normalized;
                RaycastHit2D hit;
                hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPoint, 1000);
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(hit.normal * jumpForce, ForceMode2D.Impulse); 
                Instantiate(particuleBounce, new Vector3(ContactPoint.x, ContactPoint.y, 0), Quaternion.identity);
                sound.PlaySoundEffectRandom(9, 11);

            }
        }

        if (gameObjectsCollide.Contains(other.gameObject))
        {
            gameObjectsCollide.Remove(other.gameObject);
        }
    }
}
