using System;
using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadScene : MonoBehaviour
{
    [Inject] LevelDifficulty difficulty;
    [SerializeField] private Difficulty diff;
    
    public void OpenScene(string scene)
    {
        difficulty.diff = diff;
        
        SceneManager.LoadScene(scene);
    }
}
