using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Menu : MonoBehaviour {

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    StartFirstLevel();
                    break;
                case 1:
                    ReturnToMainMenu();
                    break;
                default:

                    break;
            }
        }
    }

    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
