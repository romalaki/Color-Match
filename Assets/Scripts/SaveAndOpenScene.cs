using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndOpenScene : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    public void OpenScene(string scene)
    { 
        scoreManager.SaveScore();
        SceneManager.LoadScene(scene);
    }
}
