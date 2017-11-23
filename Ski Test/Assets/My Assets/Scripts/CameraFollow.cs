﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    	
	// Update is called once per frame
	void FixedUpdate () {
        if (player)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 5);
            transform.LookAt(player.transform);
        
        }
    }
  }
