using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float input, speed = 5f, jump = 12.5f;
    private bool facingLeft;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public LayerMask ground;
    public Animator animator;
    public bool fallThrough;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(groundCheck());
        input = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(input * speed, rb.linearVelocity.y);

        if (input < -0.01f)
        {
            facingLeft = true;
        }
        else if (input > 0.01f)
        {
            facingLeft = false;
        }

        animator.SetFloat("Horizontal", input);
        animator.SetFloat("Speed", Mathf.Abs(input));
        // Speed is a general checker if the player is moving, regardless of direction
        // Horizontal will determine where the player faces, -1 for left, 1 for right

        if (Mathf.Abs(animator.GetFloat("Horizontal")) > 0.00)
            animator.SetFloat("LastHorizontal", input);
        // LastHorizontal takes the direction the player faces when it moves and faces
        // that same way when they stop moving and are idle
        // it is defaulted at 1(right) because we want the player to start in idle right

        if (Input.GetKey(KeyCode.Space) && groundCheck())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);

        // boolean fallThrough checks if the player is trying to go down a one way platform
        if (Input.GetKeyDown(KeyCode.S))
        {
            fallThrough = true;
        }
        else
        {
            fallThrough = false;
        }
    }

    private bool groundCheck()
    {
        RaycastHit2D bottomCheck = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.01f, ground);
        if (bottomCheck)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        return bottomCheck;
    }

}
