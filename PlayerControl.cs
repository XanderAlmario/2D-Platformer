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

    // Update is called once per frame
    void Update()
    {
        Debug.Log(groundCheck());
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

        if (Input.GetKey(KeyCode.Space) && groundCheck())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
    }

    private bool groundCheck()
    {
        RaycastHit2D bottomCheck = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.01f, ground);
        if (bottomCheck)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        return bottomCheck;
    }
}