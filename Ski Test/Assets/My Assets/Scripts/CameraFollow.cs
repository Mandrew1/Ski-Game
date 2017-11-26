﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    	
	// Update is called once per frame
	void LateUpdate () {
        transform.LookAt(player.transform, transform.forward);
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 5);
            
    }
  }
