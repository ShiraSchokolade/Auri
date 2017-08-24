using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
  private PlayerController player;
  private MoonConnection moonConnection;

  [Header("Skills")]
  public Button toggleConnectionButton;
  public Text toggleConnectionButtonText;
  public Sprite cutConnection;
  public Sprite createConnection;

  [Header("Eneru")]
  public Text eneruText;
  public Image eneruFilling;
  public int yPositionEneruFull = -35;
  public int yPositionEneruEmpty = -75;

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

    eneruFilling.transform.position =   new Vector3(fillingStartVector.x, player.eneru* 0.5f - Mathf.Abs(fillingStartVector.y)-50f, fillingStartVector.z);
  }


  void UpdateEneruUI()
  {
    eneruText.text = "Eneru: " + player.eneru.ToString();

  }

  public void ToggleConnection()
  {
    player.isConnected = !player.isConnected;

    if (player.isConnected)
    {
      toggleConnectionImage.sprite = cutConnection;
      toggleConnectionButtonText.text = "Cut";
    }
    else
    {
      toggleConnectionImage.sprite = createConnection;
      toggleConnectionButtonText.text = "Create";
    }
    moonConnection.ToggleMoonString();
  }
}
