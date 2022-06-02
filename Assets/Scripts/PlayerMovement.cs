using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float JumpSpeed = 5f;

    [SerializeField] Vector2 DeadMove = new Vector2 (20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Vector2 InputMove;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float GravityScaleAtStart;

    bool isAlive = true;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();


        GravityScaleAtStart = myRigidbody.gravityScale;
        
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        myAnimator.SetTrigger("Shot");
        Instantiate(bullet, gun.position, transform.rotation);

    }



    void Update()
    {
        if(!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Traps"))) 
        {
            isAlive = false;
            myAnimator.SetTrigger("Dead");
            myRigidbody.velocity = DeadMove;
            FindObjectOfType<GameSession>().DEATH();
        }
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
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        { 
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = GravityScaleAtStart;
            return;
        }
        myRigidbody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, InputMove.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;

        bool PlayerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", PlayerHasVerticalSpeed);

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
        if(!isAlive) return;
        InputMove = Value.Get<Vector2>();
        Debug.Log(InputMove);
    }
    void OnJump(InputValue Value)
    {
        if(!isAlive) return;
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
        if(Value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, JumpSpeed);
        }
        }
        
    }
}
