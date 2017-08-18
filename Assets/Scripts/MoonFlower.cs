using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFlower : MonoBehaviour {

    public int eneruRaise = 50;
    private PlayerController player;

    void Start () {

        player = GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<PlayerController>();
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Constants.TAG_PLAYER)
            player.RaiseEneru(eneruRaise);
    }
}
