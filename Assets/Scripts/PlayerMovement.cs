using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    float horizontalValue;
    [SerializeField] float movementSpeed;

    bool canJump;
    bool isJumping;
    [SerializeField] float jumpForce;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        if (horizontalValue > 0) sr.flipX = false;
        else if (horizontalValue < 0 ) sr.flipX = true;
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isJumping)
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalValue * Time.deltaTime * movementSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plateform")
        {
            isJumping = false;
            canJump = true;
            rb.gravityScale = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isJumping) canJump = false;
    }

    void Jump()
    {
        isJumping = true;
        Debug.Log("Jump");
        rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        StartCoroutine(FallTime());
    }

    IEnumerator FallTime()
    {
        yield return new WaitForSeconds(0.6f);
        rb.gravityScale = 1.75f;
       
    }
}
