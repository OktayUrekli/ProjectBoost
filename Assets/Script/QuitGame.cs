using UnityEngine;

public class QuitGame : MonoBehaviour
{  
    void Update()
    {
        quitGame();
    }

    void quitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            
        }
    }
}
