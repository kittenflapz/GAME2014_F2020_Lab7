using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RampDirection
{
    NONE,
    UP,
    DOWN
}

public class OpossumBehaviour : MonoBehaviour
{
    public float runForce;
    public Rigidbody2D rigidBody2D;
    public Transform lookInFrontPoint;
    public Transform lookAheadPoint;
    public LayerMask collisionGroundLayer;
    public LayerMask collisionWallLayer;
    public bool isGroundAhead;
    public bool isWallAhead;
    public bool onRamp;
    public RampDirection rampDirection;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rampDirection = RampDirection.NONE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _LookInFront();
        _LookAhead();
        _Move();
    }

    private void _LookInFront()
    {
       var wallHit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, collisionWallLayer);

        if (wallHit)
        {
            if (!wallHit.collider.CompareTag("Ramps"))
            {
                _FlipX();
            }
            else
            {
                rampDirection = RampDirection.UP;
            }

        }
        
        Debug.DrawLine(transform.position, lookInFrontPoint.position, isWallAhead ? Color.blue : Color.red);
    }

    private void _LookAhead()
    {
        var groundHit = Physics2D.Linecast(transform.position, lookAheadPoint.position, collisionGroundLayer);

        if (groundHit)
        {
            if(groundHit.collider.CompareTag("Ramps"))
            {
                onRamp = true;
            }

            if (groundHit.collider.CompareTag("Platforms"))
            {
                onRamp = false;
            }

            isGroundAhead = true;
        }
        else
        {
            isGroundAhead = false;
        }

        Debug.DrawLine(transform.position, lookAheadPoint.position, isGroundAhead ? Color.green : Color.red);
    }

    private void _Move()
    {
        if (isGroundAhead)
        {
            rigidBody2D.AddForce(Vector2.left * runForce * Time.deltaTime * transform.localScale.x);

            if (onRamp)
            {
                rigidBody2D.AddForce(Vector2.up * runForce * 0.5f * Time.deltaTime);

                switch (rampDirection)
                {
                    case RampDirection.UP:
                           transform.rotation = Quaternion.Euler(0.0f, 0.0f, -26.0f);
                        break;
                    case RampDirection.DOWN:
                        transform.rotation = Quaternion.Euler(0.0f, 0.0f, -26.0f);
                        break;
                    case RampDirection.NONE:
                        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        break;
                }   
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            rigidBody2D.velocity *= 0.90f;
        }
        else
        {
            _FlipX();
        }

        if (isWallAhead)
        {
            _FlipX();
        }
    }

    private void _FlipX()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
  
}
