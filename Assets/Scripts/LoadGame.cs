using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private GameObject load;

    void Start()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        load.transform.Rotate(0f, 0f, 90f * Time.deltaTime);
    }
}
