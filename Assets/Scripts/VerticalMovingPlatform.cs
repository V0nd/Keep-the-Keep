using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    public int speed = 3;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        myRigidbody.velocity = new Vector2(0, speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            if (speed > 0)
            {
                speed = -Mathf.Abs(speed);
            }
            else if (speed < 0)
            {
                speed = Mathf.Abs(speed);
            }
        }
    }
}
