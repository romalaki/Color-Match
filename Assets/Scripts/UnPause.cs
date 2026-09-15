using UnityEngine;

public class UnPause : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private TimeManager timeManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&  timeManager.IsRunning)
        {
            UnPauseGame();
        }
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}
