using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
  private float input = 0;

  [Header("Moonstring")]
  public float moonStringSpeed = 3.3f;
  public float scaleFactor = 0.125f;
  public GameObject moonString;   // moonString Sprite
  public Vector3 moonStringStartPosition = new Vector3(9, 5, 0);
  //[HideInInspector]
  public bool isConnected = true;

  [Header("Moonstring")]
  [HideInInspector]
  public float eneru = 10f;   // 0: empty, 100: full
  private float timer = 0;
  public float tickTimeInSeconds = 1f;
  public float eneruRaisePerTick = 10f;


  void Start()
  {
    // find components
    rb = GetComponent<Rigidbody2D>();
    mainCamera = GameObject.Find(Constants.CAMERA).GetComponent<Camera>();

    // check boundaries
    if (leftBoundary != null)
      leftBound = leftBoundary.transform.position.x;
    else
      leftBound = 22;

    if (rightBoundary != null)
      rightBound = rightBoundary.transform.position.x;
    else
      rightBound = 52;

    // check Moonstring
    if (moonString == null)
      Debug.Log("Attach a moonstring sprite");
    else
      PlaceMoonstring(moonStringStartPosition);
  }


  void Update()
  {
    // groundCheck
    grounded = Physics2D.Linecast(transform.position, transform.Find(Constants.GROUNDCHECK).position, 1 << LayerMask.NameToLayer(Constants.LAYER_GROUND));

    // Move player depending on input
    input = Input.GetAxis(Constants.INPUT_HORIZON);

    // only move if there is an input
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

      // move Camera and Moonstring
      MoveCamera();
    }
    else
    {
      // stop movement of moonstring
      moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    // rotate and scale Moonstring
    RotateSprite();
    ScaleSprite();

    // Jump mechanic
    if (Input.GetButtonDown(Constants.INPUT_JUMP) && grounded)
    {
      rb.AddForce(Vector2.up * jumpForce);
    }

    UpdateEneru();
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
  /// move moonstring
  /// </summary>
  void MoveCamera()
  {
    if (transform.position.x > leftBound && transform.position.x < rightBound)
    {
      Vector3 x = new Vector3(transform.position.x, 0f, -10);
      mainCamera.transform.position = x;
      MoveMoonstring();
    }
    else
      moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
  }

  /// <summary>
  /// place moonstring at positionVector
  /// </summary>
  /// <param name="positionVector"></param>
  void PlaceMoonstring(Vector3 positionVector)
  {
    moonString.transform.position = new Vector3(positionVector.x, positionVector.y, positionVector.z);
  }

  /// <summary>
  /// add force to x-axis of moonstring sprite depending on input*moonStringSpeed
  /// </summary>
  void MoveMoonstring()
  {
    moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(input * moonStringSpeed, moonString.GetComponent<Rigidbody2D>().velocity.y);
  }

  /// <summary>
  /// scale moonString depending on distance between Auri and the moon
  /// </summary>
  void ScaleSprite()
  {
    // calculate distance between player and moonstring anker (moon)
    float distance = Vector2.Distance(moonString.transform.position, transform.position);
    moonString.transform.localScale = new Vector2(moonString.transform.localScale.x, distance * scaleFactor);
  }

  /// <summary>
  /// rotate sprite, so that it is orientated to the player
  /// </summary>
  void RotateSprite()
  {
    Vector3 lookAtVector = moonString.transform.position - transform.position;
    float angle = Mathf.Rad2Deg * Mathf.Atan2(lookAtVector.y, lookAtVector.x);
    Quaternion newRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    moonString.transform.rotation = Quaternion.Slerp(moonString.transform.rotation, newRotation, Time.deltaTime * 300);
  }


  void UpdateEneru()
  {
    if (eneru > 0)
    {
      if (isConnected)
      {
        // raise eneru in ticks, if its not full
        if (eneru < 100)
        {
          if (timer < tickTimeInSeconds)
            timer += Time.deltaTime;
          else
          {
            eneru += eneruRaisePerTick;
            eneru = Mathf.Round(eneru);
            timer = 0;
          }
        }
      }

      // lower eneru in ticks, if player is not connected
      else
      {
        if (timer < tickTimeInSeconds)
          timer += Time.deltaTime;
        else
        {
          eneru -= eneruRaisePerTick;
          eneru = Mathf.Round(eneru);
          timer = 0;
        }
      }

    }
    // die when eneru is empty
    else
    {
      SceneManager.LoadScene(Constants.MAINMENU);
    }
  }

  public void ToggleMoonString()
  {
    moonString.SetActive(!moonString.activeSelf);
  }
}
