using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public LayerMask platformLayerMask;

    [Header("Movement")]
    private float moveSpeed;
    public float originalValueOfSpeed;
    private float inputHorizontal;
    private float coyoteCounter;
    private float jumpBufferCounter;

    [Header("Jump")]
    public float midAirSpeed;
    public float jumpForce;
    public float coyoteTime;
    public float jumpBufferLength;

    [Header("Particle Effects")]
    public ParticleSystem impactDust;
    private bool wasOnGround;

    [Header("Sounds")]
    public AudioManager audioManager;

    //Animations
    private enum State { idle, running, jumping, falling }
    private State state = State.idle;

    //References
    private Rigidbody2D myRigidbody;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider2d;


    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        moveSpeed = originalValueOfSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Dusts();
        CoyoteTimeCheck();
        AnimationState();
        anim.SetInteger("state", (int)state);
        Jump();
        //Debug.Log("updating");
    }

    //For physics
    private void FixedUpdate()
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

        if (Input.GetButtonDown("Jump"))
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

        if (Input.GetButtonUp("Jump") && myRigidbody.velocity.y > 0)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.6f);
        }
    }

    private void Dusts()
    {
        if (IsGrounded() && !wasOnGround)
        {
            audioManager.Play("jumpLanding");
            impactDust.gameObject.SetActive(true);
            impactDust.Stop();
            impactDust.transform.position = this.gameObject.transform.position;
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
        else if (!IsGrounded() && myRigidbody.velocity.y < 0.1f)
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
}
