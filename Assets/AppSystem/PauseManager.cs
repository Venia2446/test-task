using UnityEngine;

public class PauseManager
{
    public void TryPauseGame()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        Time.timeScale = 0f;
    }

    public void TryUnpaseGame() 
    {
        if (Time.timeScale == 1f)
        {
            return;
        }

        Time.timeScale = 1f;
    }
}
