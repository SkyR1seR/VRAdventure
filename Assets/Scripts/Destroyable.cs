using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public GameObject particles;
    public GameObject DroppedItem;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Instantiate(particles, transform.position, transform.rotation);
            Instantiate(DroppedItem, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
