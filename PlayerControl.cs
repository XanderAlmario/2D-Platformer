using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private double input;
    private float speed = 5f, jump = 12.5f;
    private bool grounded;
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(grounded);

        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space) && grounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        grounded = false;
    }
}
