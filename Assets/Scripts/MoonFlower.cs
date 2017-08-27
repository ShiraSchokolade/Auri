using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFlower : MonoBehaviour
{
  public int eneruRaise = 50;
  public int coolDown = 5;
  public Sprite enabledSprite;
  public Sprite disabledSprite;

  private PlayerController player;
  private bool interactable = true;

  void Start()
  {
    player = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<PlayerController>();

    if (enabledSprite == null || disabledSprite == null)
      Debug.Log("you didn't assign sprites to the moonflower");
  }

  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if ( interactable && col.tag == Constants.TAG_PLAYER)
    {
      player.RaiseEneru(eneruRaise);
      StartCoroutine(PerformCooldown());
    }
  }

  private IEnumerator PerformCooldown()
  {
    DisableFlower();

    //float timer = 0;

    yield return new WaitForSecondsRealtime(coolDown);

    //while(timer < coolDown)
    //{
    //  timer += Time.deltaTime;
    //}
    EnableFlower();
  }

  private void DisableFlower()
  {
    interactable = false;
    GetComponent<SpriteRenderer>().sprite = disabledSprite;
  }

  private void EnableFlower()
  {
    interactable = true;
    GetComponent<SpriteRenderer>().sprite = enabledSprite;
  }
}
