using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

  public void LoadScene()
  {
    SceneManager.LoadScene(Constants.LEVEL1);
  }

  public void Exit()
  {
    Application.Quit();
  }
}
