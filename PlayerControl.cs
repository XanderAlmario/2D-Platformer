using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float input, speed = 5f, jump = 12.5f;
    private bool doubleJump = false;
    private RaycastHit2D bottomCheck, hittingWall;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public LayerMask jumpable, wall;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(groundCheck());
        input = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(input * speed, rb.linearVelocity.y);

        animator.SetFloat("Horizontal", input);
        animator.SetFloat("Speed", Mathf.Abs(input));
        // Speed is a general checker if the player is moving, regardless of direction
        // Horizontal will determine where the player faces, -1 for left, 1 for right

        if (Mathf.Abs(animator.GetFloat("Horizontal")) > 0.00)
            animator.SetFloat("LastHorizontal", input);
        // LastHorizontal takes the direction the player faces when it moves and faces
        // that same way when they stop moving and are idle
        // it is defaulted at 1(right) because we want the player to start in idle right

        bottomCheck = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.01f, jumpable);
        hittingWall = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.01f, wall);
        if (Input.GetKeyDown(KeyCode.Space) && (bottomCheck || !doubleJump))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            if (!bottomCheck)
                doubleJump = true;
        }

        if (hittingWall)
        {
            rb.gravityScale = 0.5f;
            doubleJump = false;
        }
        else
            rb.gravityScale = 3;  

        if (bottomCheck)
        {
            doubleJump = false;
            animator.SetBool("isJumping", false);
        }
        else
            animator.SetBool("isJumping", true);
    }
}
