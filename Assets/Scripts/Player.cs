using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // Config
    [SerializeField] public float runSpeed = 5f;
    [SerializeField] public float jumpSpeed = 5f;

    // State
    private bool isAlive = true;

    // Cached component references
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private Collider2D myCollider2D;

    // Message then methods
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        Run();
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

    private void ClimbLadders()
    {
        if (myRigidbody.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(0f, controlThrow * runSpeed);
            myRigidbody.velocity += playerVelocity;
            myAnimator.SetBool("Climbing", PlayerHasVerticalSpeed());
        }
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
