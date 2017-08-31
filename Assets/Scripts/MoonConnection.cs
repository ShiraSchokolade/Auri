using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonConnection : MonoBehaviour
{
  [Header("Moonstring")]
  public float moonStringSpeed = 3.3f;
  public float scaleFactor = 0.21f;
  public float rotationOffset = -45f;
  public GameObject moonString;   // moonString Sprite
  public Vector3 moonStringStartPosition = new Vector3(9, 5, 0);
  //[HideInInspector]

  private PlayerController player;

  void Start()
  {
    // check Moonstring
    if (moonString == null)
      Debug.Log("Attach a moonstring sprite");
    else
      PlaceMoonstring(moonStringStartPosition);

    player = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<PlayerController>();
  }

  void Update()
  {

  }

  public void StopMovement()
  {
    moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
  }

  /// <summary>
  /// place moonstring at positionVector
  /// </summary>
  /// <param name="positionVector"></param>
  public void PlaceMoonstring(Vector3 positionVector)
  {
    moonString.transform.position = new Vector3(positionVector.x, positionVector.y, positionVector.z);
  }

  /// <summary>
  /// add force to x-axis of moonstring sprite depending on input*moonStringSpeed
  /// </summary>
  public void MoveMoonstring(float inputValue)
  {
    moonString.GetComponent<Rigidbody2D>().velocity = new Vector2(inputValue * moonStringSpeed, moonString.GetComponent<Rigidbody2D>().velocity.y);
  }

  /// <summary>
  /// scale moonString depending on distance between Auri and the moon
  /// </summary>
  public void ScaleSprite()
  {
    // calculate distance between player and moonstring anker (moon)
    float distance = Vector2.Distance(moonString.transform.position, player.transform.position);
    moonString.transform.localScale = new Vector2(distance * scaleFactor, distance * scaleFactor);
  }

  /// <summary>
  /// rotate sprite, so that it is orientated to the player
  /// </summary>
  public void RotateSprite()
  {
    Vector3 lookAtVector = moonString.transform.position - player.transform.position;
    float angle = Mathf.Rad2Deg * Mathf.Atan2(lookAtVector.y, lookAtVector.x);
    Quaternion newRotation = Quaternion.AngleAxis(angle + rotationOffset, Vector3.forward);
    moonString.transform.rotation = Quaternion.Slerp(moonString.transform.rotation, newRotation, Time.deltaTime * 300);
  }

  public void ToggleMoonString()
  {
    //moonString.SetActive(!moonString.activeSelf);
    Color color = moonString.GetComponent<SpriteRenderer>().color;
    if (color.a == 0)
      color.a = 100f;
    else
      color.a = 0f;

    moonString.GetComponent<SpriteRenderer>().color = color;
  }

  public void ToggleMoonString2(bool connect)
  {
    //moonString.SetActive(!moonString.activeSelf);
    Color color = moonString.GetComponent<SpriteRenderer>().color;
    if (connect)
      color.a = 100f;
    else
      color.a = 0f;

    moonString.GetComponent<SpriteRenderer>().color = color;
  }
}
