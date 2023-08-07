using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSetActiveGameObjects : MonoBehaviour
{
    public List<GameObject> ActiveObjects;
    public List<GameObject> DisactiveObjects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            for(int i = 0; i < ActiveObjects.Count; i++)
            {
                ActiveObjects[i].SetActive(true);
            }
            for(int i = 0; i < DisactiveObjects.Count; i++)
            {
                DisactiveObjects[i].SetActive(false);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
}
