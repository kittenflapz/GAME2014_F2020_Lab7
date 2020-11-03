using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform SpawnPoint;

    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_spireRenderer;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spireRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    void _Move()
    {
       
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {
                m_spireRenderer.flipX = false;
                m_rigidbody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {
                m_rigidbody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                m_spireRenderer.flipX = true;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Vertical > joystickVerticalSensitivity)
            {
                    if (isGrounded)
                    {
                        m_rigidbody2D.AddForce(Vector2.up * verticalForce * Time.deltaTime);
                        m_animator.SetInteger("AnimState", 2);
                    }
            }
            else
             {
                    
                   // idle
                   m_animator.SetInteger("AnimState", 0);
                
             }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.position = SpawnPoint.position;

    }
}
