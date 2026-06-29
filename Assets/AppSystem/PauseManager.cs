using UnityEngine;

public class PauseManager
{

    public void TryPauseGame()
    {
        if (IsPaused)
        {
            return;
        }

        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void TryUnpaseGame() 
    {
        if (!IsPaused)
        {
            return;
        }

        IsPaused = false;
        Time.timeScale = 1f;
    }

    private bool IsPaused { get; set; }

}
