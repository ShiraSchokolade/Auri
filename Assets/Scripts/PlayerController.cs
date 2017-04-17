using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 800;
    public float maxSpeed = 15;
    
    private bool grounded = true;
    private Rigidbody2D rb;
    private bool facingRight = true;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        //grounded = (rb.velocity.y <= 0.1);
        grounded = Physics2D.Linecast(transform.position, transform.Find("GroundCheck").position, 1 << LayerMask.NameToLayer("Ground"));

        float input = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(input * maxSpeed, rb.velocity.y);

        if (input > 0 && !facingRight) {
            Flip();
        }
        else if (input < 0 && facingRight) {
            Flip();
        }


        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
