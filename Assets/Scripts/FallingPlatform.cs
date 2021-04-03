using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Vector2 startPosition;
    public int spawnBackDelay = 1;
    public LayerMask platformMask;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.ToLower().Contains("platform"))
        {
            this.gameObject.SetActive(false);
            Invoke("Respawn", spawnBackDelay);
        }
    }

    private void Respawn()
    {
        this.gameObject.transform.position = startPosition;
        this.gameObject.SetActive(true);
        Debug.Log("respawn");
    }
}
