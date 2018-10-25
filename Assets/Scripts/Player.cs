using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // Config
    [SerializeField] public float runSpeed = 5f;
    [SerializeField] public float jumpSpeed = 5f;
    [SerializeField] public float climbSpeed = 5f;

    // State
    private bool isAlive = true;

    // Cached component references
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private Collider2D myCollider2D;
    private float gravityScaleAtStart;

    // Message then methods
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update ()
    {
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        myAnimator.SetBool("Running", PlayerHasHorizontalSpeed());
    }

    private void ClimbLadder()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }
        myRigidbody.gravityScale = 0;
        Debug.Log("Touching Ladder");
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myAnimator.SetBool("Climbing", PlayerHasVerticalSpeed());
    }

    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
            
        }
    }

    private void FlipSprite()
    {
        if(PlayerHasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private bool PlayerHasHorizontalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    }

    private bool PlayerHasVerticalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    }
}
