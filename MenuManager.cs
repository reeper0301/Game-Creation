using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnPressGameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnPressExit()
    {
        Application.Quit();
    }
}
