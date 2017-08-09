using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Player")]
  // public variables
  public float jumpForce = 800;
  public float maxSpeed = 15;

  // side boundaries
  public GameObject leftBoundary;
  public GameObject rightBoundary;
  private float leftBound;
  private float rightBound;

  // movement
  private bool grounded = true;
  private Rigidbody2D rb;
  private bool facingRight = true;
  private Camera mainCamera;
  float input = 0;

  // Moonstring
  [Header("Moonstring")]
  public float moonStringSpeed = 3.3f;
  public float scaleFactor = 0.125f;
  public GameObject moonString;   // moonString Sprite
  public GameObject moon;         // moon, far behind in Z
  public Vector3 moonStringStartPosition = new Vector3(9, 5, 0);


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    mainCamera = GameObject.Find(Constants.CAMERA).GetComponent<Camera>();

    if (leftBoundary != null)
      leftBound = leftBoundary.transform.position.x;
    else
      leftBound = 22;

    if (rightBoundary != null)
      rightBound = rightBoundary.transform.position.x;
    else
      rightBound = 52;

    //Moonstring
    if (moonString == null)
      Debug.Log("Attach a moonstring sprite");
    else if (moon == null)
      Debug.Log("Attach a moon gameObject");
    else
      PlaceMoonstring(moonStringStartPosition);
  }


  void Update()
  {
    // groundCheck
    grounded = Physics2D.Linecast(transform.position, transform.Find(Constants.GROUNDCHECK).position, 1 << LayerMask.NameToLayer(Constants.LAYER_GROUND));

    // Move player depending on input
    input = Input.GetAxis(Constants.INPUT_HORIZON);

    if (input != 0)
    {
      rb.velocity = new Vector2(input * maxSpeed, rb.velocity.y);

      // Flip image depending on moving direction
      if (input > 0 && !facingRight)
      {
        Flip();
      }
      else if (input < 0 && facingRight)
      {
        Flip();
      }

      // move Camera
      MoveCamera();
      RotateSprite();
      ScaleSprite();
    }
    else
    {
      moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    // Jump mechanic
    if (Input.GetButtonDown(Constants.INPUT_JUMP) && grounded)
    {
      rb.AddForce(Vector2.up * jumpForce);
    }
  }

  /// <summary>
  /// inverts the image on the x-axis
  /// </summary>
  void Flip()
  {
    facingRight = !facingRight;
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
  }

  /// <summary>
  /// moves camera in between left and right Bounds
  /// </summary>
  void MoveCamera()
  {
    if (transform.position.x > leftBound && transform.position.x < rightBound)
    {
      Vector3 x = new Vector3(transform.position.x, 0f, -10);
      mainCamera.transform.position = x;
      PlaceMoonstring();
    }
    else
      moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
  }

  void PlaceMoonstring(Vector3 positionVector)
  {
    moonString.transform.position = new Vector3(positionVector.x, positionVector.y, positionVector.z);
  }

  void PlaceMoonstring()
  {
    //moonString.transform.position = new Vector3(moonString.transform.position.x + input*moonStringSpeed, moonString.transform.position.y, moonString.transform.position.z);   
    moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(input * moonStringSpeed, moonString.GetComponent<Rigidbody2D>().velocity.y);
  }

  void ScaleSprite()
  {
    // calculate distance between player and moonstring anker (moon)
    float distance = Vector2.Distance(moonString.transform.position, transform.position);
    print(distance);
    moonString.transform.localScale = new Vector2(moonString.transform.localScale.x, distance * scaleFactor);
  } 

  void RotateSprite()
  {
    Vector3 lookAtVector = moonString.transform.position - transform.position;
    float angle = Mathf.Rad2Deg * Mathf.Atan2(lookAtVector.y, lookAtVector.x);
    Quaternion newRotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    moonString.transform.rotation = Quaternion.Slerp(moonString.transform.rotation, newRotation, Time.deltaTime * 300);
  }
}
