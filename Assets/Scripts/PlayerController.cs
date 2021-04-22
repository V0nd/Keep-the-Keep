using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask platformLayerMask;

    [Header("Movement")]
    private float moveSpeed;
    public float originalValueOfSpeed;
    private float coyoteCounter;
    private float jumpBufferCounter;

    private bool onMovingPlatform = false;
    public GameObject movingPlatform;

    [Header("Jump")]
    public float midAirSpeed;
    public float jumpForce;
    public float coyoteTime;
    public float jumpBufferLength;

    [Header("Particle Effects")]
    public ParticleSystem impactDust;
    private bool wasOnGround;
    private bool impacting = true;
    private bool onRotatingPlatform;

    [Header("Sounds")]
    public AudioManager audioManager;

    //Animations
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;

    //References
    private Rigidbody2D myRigidbody;
    private Animator anim;
    private BoxCollider2D boxCollider2d;


    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        moveSpeed = originalValueOfSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Dusts();
        CoyoteTimeCheck();
        AnimationState();
        anim.SetInteger("state", (int)state);
        Jump();
    }

    //For physics
    private void FixedUpdate()
    {
        Movement();
        MovementOnMovingPlatform();
    }

    private void Movement()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        if (inputHorizontal < 0)
        {
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
            else if(IsGrounded())
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }
        }
    }

    private void FootstepSoundEffect()
    {
        audioManager.Play("footstep");
    }

    private void CoyoteTimeCheck()
    {
        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferLength;
        } 
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (jumpBufferCounter >= 0f && coyoteCounter > 0f)
        {
            audioManager.Play("jump");
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
            state = State.jumping;
        }

        if(Input.GetButtonUp("Jump") && myRigidbody.velocity.y > 0)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.6f);
        }
    }

    private void Dusts()
    {
        if(IsGrounded() && !wasOnGround && !boxCollider2d.CompareTag("Rotating Platform"))
        {
            audioManager.Play("jumpLanding");
            impactDust.gameObject.SetActive(true);
            impactDust.Stop();
            impactDust.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.5f, this.gameObject.transform.position.z);
            impactDust.Play();
        }

        wasOnGround = IsGrounded();
    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.1f; //Raycast need to be outside of player
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        return raycastHit.collider != null;
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (myRigidbody.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (IsGrounded())
            {
                state = State.idle;
            }
        }
        else if(!IsGrounded() && myRigidbody.velocity.y < 0.1f)
        {
            state = State.falling;
        }
        else if (Mathf.Abs(myRigidbody.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Moving Platform"))
        {
            movingPlatform = collision.gameObject;
            onMovingPlatform = true;
        }

        if(collision.gameObject.CompareTag("Rotating Platform"))
        {
            impacting = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            movingPlatform = null;
            onMovingPlatform = false;
        }

        if(collision.gameObject.CompareTag("Rotating Platform"))
        {
            impacting = true;
        }
    }

    private void MovementOnMovingPlatform()
    {
        if(onMovingPlatform)
        {
            myRigidbody.velocity += movingPlatform.gameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
