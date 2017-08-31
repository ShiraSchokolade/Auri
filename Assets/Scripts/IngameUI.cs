﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
  private PlayerController player;
  private MoonConnection moonConnection;

  [Header("Skills")]
  public Button toggleConnectionButton;
  public Sprite cutConnection;
  public Sprite createConnection;

  [Header("Eneru")]
  public Text eneruText;
  public Image eneruFilling;
  public int yPositionEneruFull = -35;
  public int yPositionEneruEmpty = -75;
  public float yOffset = -50f;

  private Image toggleConnectionImage;
  private Vector3 fillingStartVector;


  void Start()
  {

    player = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<PlayerController>();
    moonConnection = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<MoonConnection>();

    toggleConnectionImage = toggleConnectionButton.GetComponent<Image>();

    fillingStartVector = eneruFilling.transform.position;

    UpdateEneruUI();
  }

  void Update()
  {
    UpdateEneruUI();

    if (Input.GetButtonDown(Constants.INPUT_TOGGLE_CONNECTION))
      ToggleConnection();

    eneruFilling.transform.position = new Vector3(fillingStartVector.x, player.eneru * 0.5f - Mathf.Abs(fillingStartVector.y) + yOffset, fillingStartVector.z);
  }


  void UpdateEneruUI()
  {
    eneruText.text = "Eneru: " + player.eneru.ToString();
  }

  public void ToggleConnection()
  {
    // das hier sollte in der toggle moonstring methode oder innem enstprechenden delegat stattfinden
    if (!player.isConnected && player.connectionPossible)
    {
      moonConnection.ToggleMoonString2(true);
      player.isConnected = true;
    }
    else if (player.isConnected)
    {
      player.isConnected = false;
      moonConnection.ToggleMoonString2(false);
    }
    //player.isConnected = !player.isConnected;  

    //if (player.isConnected)
    //{
    //  //toggleConnectionImage.sprite = cutConnection;
    //}
    //else
    //{
    //  //toggleConnectionImage.sprite = createConnection;
    //}

    //moonConnection.ToggleMoonString();
  }

  public void CreateConnection()
  {
    if (!player.isConnected && player.connectionPossible)
    {
      player.isConnected = true;
      moonConnection.ToggleMoonString2(true);
    }
  }

  public void CutConnection()
  {
    player.isConnected = false;
    moonConnection.ToggleMoonString2(false);
  }

  public void GoToMenu()
  {
    SceneManager.LoadScene(Constants.MAINMENU);
  }
}
