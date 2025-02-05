using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void OnPressTryAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnPressLeaveToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
