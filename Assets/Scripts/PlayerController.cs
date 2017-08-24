using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  [Header("Movement")]
  // public variables
  public float jumpForce = 800;
  public float maxSpeed = 4f;
  public float slowSpeed = 1f;

  // side boundaries
  public GameObject leftBoundary;
  public GameObject rightBoundary;
  private float leftBound;
  private float rightBound;

  // movement
  private float speed;
  private bool grounded = true;
  private Rigidbody2D rb;
  private bool facingRight = true;
  private Camera mainCamera;
  private float input = 0;

  [Header("Eneru")]
  //[HideInInspector]
  public int eneru = 10;   // 0: empty, 100: full

  private float timer = 0;
  public float tickTimeInSeconds = 1f;
  public int eneruRaisePerTick = 10;
  public int eneruMax = 100;
  public int eneruTreshhold = 40;
  public float minAlphaValue = 0.4f;

  private MoonConnection moonConnection;
  public bool isConnected = true;

  private float oldAuriPosition;
  private float currentAuriPosition;
    

  void Start()
  {
    // find components
    rb = GetComponent<Rigidbody2D>();
    mainCamera = GameObject.Find(Constants.CAMERA).GetComponent<Camera>();
    moonConnection = GetComponent<MoonConnection>();

    // check boundaries
    if (leftBoundary != null)
      leftBound = leftBoundary.transform.position.x;
    else
      leftBound = 22;

    if (rightBoundary != null)
      rightBound = rightBoundary.transform.position.x;
    else
      rightBound = 52;

    speed = slowSpeed;
    oldAuriPosition = currentAuriPosition = transform.position.x;
  }


  void Update()
  {
    // save currrent x-position
    currentAuriPosition = transform.position.x;

    // groundCheck
    grounded = Physics2D.Linecast(transform.position, transform.Find(Constants.GROUNDCHECK).position, 1 << LayerMask.NameToLayer(Constants.LAYER_GROUND));

    // Move player depending on input
    input = Input.GetAxis(Constants.INPUT_HORIZON);

    // only move if there is an input
    if (input != 0)
    {
      rb.velocity = new Vector2(input * speed, rb.velocity.y);

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

      oldAuriPosition = transform.position.x;
    }
    else
    {
      // stop movement of moonstring
      moonConnection.StopMovement();
    }

    // rotate and scale Moonstring
    moonConnection.RotateSprite();
    moonConnection.ScaleSprite();

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
  /// moves camera in between left and right Bounds,
  /// move moonstring
  /// </summary>
  void MoveCamera()
  {
    if (transform.position.x > leftBound && transform.position.x < rightBound)
    {
      Vector3 x = new Vector3(transform.position.x, 0f, -10);
      mainCamera.transform.position = x; 
      moonConnection.MoveMoonstring(CalculatePositionChange());
    }
    else
      moonConnection.StopMovement();
  }

  float CalculatePositionChange()
  {
    float positionChange =  currentAuriPosition - oldAuriPosition;
    return positionChange;
  }

  void UpdateEneru()
  {
    if (eneru > 0)
    {
      if (isConnected)
      {
        // raise eneru in ticks, if its not full
        if (eneru < eneruMax)
        {
          if (timer < tickTimeInSeconds)
            timer += Time.deltaTime;
          else
          {
            RaiseEneru(eneruRaisePerTick);
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
          LowerEneru(eneruRaisePerTick);
          timer = 0;
        }
      }
    }
    // die when eneru is empty
    else
    {
      SceneManager.LoadScene(Constants.MAINMENU);
    }

    // check if eneru reached a threshold which influences movement speed for example
    CheckEneruTreshholds();
  }

  public void RaiseEneru(int amount)
  {
    if (eneru < eneruMax)
    {
      eneru += amount;
      if (eneru > eneruMax)
        eneru = eneruMax;
    }
  }

  private void LowerEneru(int amount)
  {
    eneru -= amount;
  }

  private void CheckEneruTreshholds()
  {
    if (eneru > eneruTreshhold)
    {
      speed = maxSpeed;
      FadeInAuri();
    }
    else
    {
      speed = slowSpeed;
      FadeOutAuri();
    }
  }

  private void FadeOutAuri()
  {
    Color color = GetComponent<SpriteRenderer>().color;

    if (color.a > minAlphaValue)
    {
      color.a = (eneru / 10f) * 0.2f;
      GetComponent<SpriteRenderer>().color = color;
    }
    else
    {
      color.a = (eneru / 10f) * 0.5f;
      GetComponent<SpriteRenderer>().color = color;
    }
  }

  private void FadeInAuri()
  {
    Color color = GetComponent<SpriteRenderer>().color;

    color.a = 100f;
    GetComponent<SpriteRenderer>().color = color;
  }
}
