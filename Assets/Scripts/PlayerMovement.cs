using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float JumpSpeed = 5f;
    Vector2 InputMove;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }
    void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x),  1f);
        }
    }
    void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { return;}
        
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, InputMove.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;

    }
    void Run()
    {
        Vector2 PlayerVelocity = new Vector2(InputMove.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = PlayerVelocity;


        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", PlayerHasHorizontalSpeed);
    }
    void OnMove(InputValue Value)
    {
        InputMove = Value.Get<Vector2>();
        Debug.Log(InputMove);
    }
    void OnJump(InputValue Value)
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
        if(Value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, JumpSpeed);
        }
        }
        
    }
}
