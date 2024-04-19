using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("TowerUpgrade");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Encyclopedia()
    {
        SceneManager.LoadScene("Encyclopedia");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
