using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    

    [SerializeField]
    GameObject playerPrefab;

    GameObject player;

    GameObject spawnPoint;

    void Awake()
    {
        spawnPoint = gameObject;

        InitialSpawn();
      
    }

    void Respawn()
    {
        if (player.transform.position != spawnPoint.transform.position)
        {
            player.transform.position = spawnPoint.transform.position;
           
        } else
        {
            return;
        }
    }

    void InitialSpawn()
    {
        if (player == null)
        {

            if (spawnPoint != null)
            {
                player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 90, 0)));
                player.name = playerPrefab.name;
                SetUpPlayerDependencies();
            }
            
        }
    }

    void SetUpPlayerDependencies()
    {
        if (player != null)
        {
            
            //Camera Follow
            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<CameraFollow>().player = player;

            //Slope
            if (GameObject.Find("Slope") != null)
            {
            
                if (player.GetComponent<Movement>() != null)
                {
                   
                    player.GetComponent<Movement>().slope = GameObject.Find("Slope");
                }
                
            }
           
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        } 
    }

}
