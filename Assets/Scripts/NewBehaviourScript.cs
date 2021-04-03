using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public LayerMask platformLayerMask;
    private float inputHorizontal;
    private Rigidbody2D myRigidbody;
    private float moveSpeed = 4;
    private BoxCollider2D boxCollider2d;


    // Start is called before the first frame update
    void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        if (inputHorizontal < 0)
        {
            Debug.Log("Moving");
            myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            if (inputHorizontal > 0)
            {
                myRigidbody.velocity = new Vector2(+moveSpeed, myRigidbody.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
            else if (IsGrounded())
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }
        }
    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        return raycastHit.collider != null;
    }
}
