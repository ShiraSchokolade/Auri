using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour {

  private PlayerController player;
  public Text eneruText;
  public Text toggleConnectionButtonText;


  void Start () {

    player = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<PlayerController>();

    UpdateEneruUI();
  }
	
	void Update () {

    UpdateEneruUI();
	}

  void UpdateEneruUI()
  {
    eneruText.text = "Eneru: " + player.eneru.ToString();
  }

  public void ToggleConnection()
  {
    player.isConnected = !player.isConnected;

    if (player.isConnected)
      toggleConnectionButtonText.text = "Cut";
    else
      toggleConnectionButtonText.text = "Create";

    player.ToggleMoonString();
  }
}
