using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour {
    public Text speed;
    private GameObject player;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("Player") == null)
        {
            Application.Quit();
        } else
        {
            player = GameObject.Find("Player");
        }

		if (speed == null) {
            print("Text error");
            Application.Quit();
        } else
        {
            speed.text = "0";
        }
	}
	
	// Update is called once per frame
	void Update () {
        speed.text = "Speed: " + player.GetComponent<Rigidbody>().velocity.z;
	}
}
