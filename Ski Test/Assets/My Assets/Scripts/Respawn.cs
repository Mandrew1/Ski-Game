using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    [SerializeField]
    private GameObject player;

    private GameObject playerPrefab;
	// Use this for initialization
	void Awake() {

            Vector3 spawnLoc = GameObject.Find("Spawn Point").transform.position;
            Quaternion spawnRotation = GameObject.Find("Slope").transform.rotation;
            playerPrefab = Instantiate(player, spawnLoc, spawnRotation);
            playerPrefab.name = player.name;


            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<CameraFollow>().player = playerPrefab;

            player.GetComponent<Movement>().slope = GameObject.Find("Slope");
    }

    void Spawn(GameObject player)
    {
        Destroy(player);

        if (!GameObject.Find("Player"))
        {
            Vector3 spawnLoc = GameObject.Find("Spawn Point").transform.position;
            Quaternion spawnRotation = GameObject.Find("Slope").transform.rotation;
            playerPrefab = Instantiate(player, spawnLoc, spawnRotation);
            playerPrefab.name = player.name;


            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<CameraFollow>().player = playerPrefab;

            player.GetComponent<Movement>().slope = GameObject.Find("Slope");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            Spawn(playerPrefab);
        }
	}
}
