using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTrigger : MonoBehaviour {
  private PlayerController player;
  private Collider2D colli;
  private float xOffset = 2f;

  public GameObject moon;

  void Start () {
    player = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<PlayerController>();
    colli = GetComponent<Collider2D>();
  }


  void Update () {
    // change to broadcasts or delegates!
    ToggleCollision();

    //temp -> perfomanter machen
    CheckIfPlayerInCave();
	}

  //private void OnCollision2D(Collider2D col)
  //{
  //  print("bla");
  //  if (col.tag == Constants.TAG_PLAYER)
  //  {
  //    if (!player.isConnected)
  //      GetComponent<Collider2D>().isTrigger = true;
  //  }
  //}

  private void ToggleCollision()
  {
    if (player.isConnected)
      colli.isTrigger = false;
    else
      colli.isTrigger = true;
  }

  // performanter machen!
  private void CheckIfPlayerInCave()
  {
    if (player.transform.position.x > (transform.position.x + xOffset))
      player.connectionPossible = false;
    else
      player.connectionPossible = true;

    if(player.transform.position.x > (transform.position.x + 8f))
    {
      Color col = moon.GetComponent<SpriteRenderer>().color;
      col.a = 0;
      moon.GetComponent<SpriteRenderer>().color = col;
    }
    else
    {
      Color col = moon.GetComponent<SpriteRenderer>().color;
      col.a = 100f;
      moon.GetComponent<SpriteRenderer>().color = col;
    }

  }

}
