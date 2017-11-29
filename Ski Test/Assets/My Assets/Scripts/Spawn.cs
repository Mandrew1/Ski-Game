using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    

    [SerializeField]
    GameObject playerPrefab;

    GameObject player;

    GameObject spawnPoint;

    [HideInInspector]
    public GameState gameState;
    void Awake()
    {
        spawnPoint = gameObject;

        InitialSpawn();
        if (player != null || spawnPoint != null)
        {
            gameState = GameState.Error;
        } else
        {
            gameState = GameState.Active;
        }
    }

    void Respawn()
    {
        if (player.transform.position != spawnPoint.transform.position && gameState == GameState.Active)
        {
            player.transform.SetPositionAndRotation(spawnPoint.transform.position, Quaternion.identity);
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
                player = Instantiate(playerPrefab, spawnPoint.transform);
                SetUpPlayerDependencies();
            }
            
        }
    }

    void SetUpPlayerDependencies()
    {
        if (player != null)
        {
            Debug.Log("Player isn't null");
            //Camera Follow
            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<CameraFollow>().player = player;

            //Slope
            if (GameObject.Find("Slope") != null)
            {
                Debug.Log("Slop isn't null");
                if (player.GetComponent<Movement>() != null)
                {
                    Debug.Log("Movement isn't null");
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
