using UnityEngine;

public class ShowDiff : MonoBehaviour
{
    [SerializeField] private GameObject Difficulty;

    public void Show()
    {
        bool set = Difficulty.active;
        Difficulty.SetActive(!set);
    }
    
}
