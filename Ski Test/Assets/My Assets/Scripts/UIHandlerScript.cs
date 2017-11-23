using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandlerScript : MonoBehaviour {

	public void OnStartGameClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnQuitGameClick()
    {
        Application.Quit();
    }

}
