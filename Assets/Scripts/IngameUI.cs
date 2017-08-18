using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour {

  private PlayerController player;
    private MoonConnection moonConnection;
  public Text eneruText;
  public Text toggleConnectionButtonText;


  void Start () {

    player = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<PlayerController>();
        moonConnection = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<MoonConnection>();

        UpdateEneruUI();
  }
	
	void Update () {

    UpdateEneruUI();

        if (Input.GetButtonDown(Constants.INPUT_TOGGLE_CONNECTION))
            ToggleConnection();

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

    moonConnection.ToggleMoonString();
  }
}
