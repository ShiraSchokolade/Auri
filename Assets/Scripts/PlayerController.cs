using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 800;
    public float maxSpeed = 15;
    public bool movableCamera = true;

  public GameObject leftBoundary;
  public GameObject rightBoundary;
  private float leftBound;
  private float rightBound;
    
    private bool grounded = true;
    private Rigidbody2D rb;
    private bool facingRight = true;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    if (leftBoundary != null)
      leftBound = leftBoundary.transform.position.x;
    else
      leftBound = 22;

    if (rightBoundary != null)
      rightBound = rightBoundary.transform.position.x;
    else
      rightBound = 52;

  }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, transform.Find("GroundCheck").position, 1 << LayerMask.NameToLayer("Ground"));

        float input = Input.GetAxis("Horizontal");

        // Move player depending on input
        rb.velocity = new Vector2(input * maxSpeed, rb.velocity.y);

        // Flip image depending on moving direction
        if (input > 0 && !facingRight) {
            Flip();
        }
        else if (input < 0 && facingRight) {
            Flip();
        }

        // Jump mechanic
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (movableCamera) {
            moveCamera();
        }
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    void moveCamera()
    {
        if (transform.position.x > leftBound && transform.position.x < rightBound)
        {
            Vector3 x = new Vector3(transform.position.x, 0f, -10);
            GameObject.Find("Main Camera").transform.position = x;
        }
    }
}
