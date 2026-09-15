using Scripts;
using TMPro;
using UnityEngine;
using Zenject;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ScoreText;
    
    [Inject]
    public IScoreService scoreService;
    
    void Start()
    {
        ScoreText.text = "Best score: " + scoreService.BestScore();
    }
}
