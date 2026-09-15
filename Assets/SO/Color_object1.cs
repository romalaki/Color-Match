using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Color Match/Difficulty")]
public class Difficulty : ScriptableObject
{
    public int give;
    public int take;
    public int time;
}
