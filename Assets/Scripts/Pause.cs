using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TimeManager timeManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&  timeManager.IsRunning)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    void OnApplicationPause(bool paused)
    {
        if (paused &&  timeManager.IsRunning)
        {
            PauseGame();
        }
    }
}
