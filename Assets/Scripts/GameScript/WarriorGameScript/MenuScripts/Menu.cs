using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private int thisScene;
    void Awake()
    {
        thisScene = SceneManager.GetActiveScene().buildIndex;
        if (thisScene == 0) CoinManager.coinsCount = 0;
    }
    
    public void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > (thisScene+1) )
        {
            SceneManager.LoadScene(thisScene + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(thisScene);
    }

    public void BackToMenu()
    { 
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();        
#endif
    }
}
