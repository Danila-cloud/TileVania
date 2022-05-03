using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Vector2 InputMove;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        Run();
        FlipSprite();
    }
    void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x),  1f);
        }
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
}
